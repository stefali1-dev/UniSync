import { Component, OnInit } from '@angular/core';
import { VexScrollbarComponent } from '@vex/components/vex-scrollbar/vex-scrollbar.component';
import { AsyncPipe, NgFor } from '@angular/common';
import { CommonModule } from '@angular/common';

import { MatButtonModule } from '@angular/material/button';
import { MatInputModule } from '@angular/material/input';
import { UserService } from '../../../_services/user.service';
import { MatDialogModule } from '@angular/material/dialog';
import { BehaviorSubject } from 'rxjs';
import { ChatService } from '../chat.service';
import { StorageService } from '../../../_services/storage.service';
import { Router } from '@angular/router';
import { ChannelService } from '../../../_services/channel.service';
import { ViewChild, ElementRef } from '@angular/core';
import { ChannelCreationDto } from 'src/app/_interfaces/channelCreationDto';

export interface Contact {
  id: number;
  imageSrc: string;
  name: string;
  email: string;
  role: string;
  selected: boolean;
}

@Component({
  selector: 'dialog-group-list',
  templateUrl: 'dialog-group-list.html',
  standalone: true,
  imports: [
    MatDialogModule,
    MatButtonModule,
    VexScrollbarComponent,
    MatInputModule,
    NgFor,
    AsyncPipe,
    CommonModule
  ]
})
export class DialogGroupList implements OnInit {
  @ViewChild('groupNameInput') groupNameInput!: ElementRef;

  contacts: Contact[] = [];

  private contactsSubject = new BehaviorSubject<Contact[]>(this.contacts);
  public contacts$ = this.contactsSubject.asObservable();

  selectedContacts: Contact[] = [];

  private selectedContactsSubject = new BehaviorSubject<Contact[]>(
    this.selectedContacts
  );
  public selectedContacts$ = this.selectedContactsSubject.asObservable();

  constructor(
    private userService: UserService,
    private chatService: ChatService,
    private storageService: StorageService,
    private router: Router,
    private channelService: ChannelService
  ) {}

  ngOnInit() {
    this.getInitialContacts(10);
  }

  onEnterPressed(request: string) {
    this.userService.getSearchedUsers(request).subscribe({
      next: (data) => {
        console.log(data.users);

        let fetchedUsers: Contact[] = [];

        data.users.forEach((user) => {
          let contact: Contact = {
            id: user.userId,
            imageSrc: user.userPhoto,
            name: user.firstName + ' ' + user.lastMessage,
            email: user.email,
            role: 'student',
            selected: false
          };

          fetchedUsers.push(contact);
        });

        this.contacts = fetchedUsers;
        this.contactsSubject.next(this.contacts);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getInitialContacts(numberOfContacts: number) {
    this.userService.getAllUsers().subscribe({
      next: (data) => {
        let fetchedUsers: Contact[] = [];

        data.users.slice(0, numberOfContacts).forEach((user) => {
          let contact: Contact = {
            id: user.userId,
            imageSrc: user.userPhoto,
            name: user.firstName + ' ' + user.lastMessage,
            email: user.email,
            role: 'student',
            selected: false
          };

          fetchedUsers.push(contact);
        });

        this.contacts = fetchedUsers;
        this.contactsSubject.next(this.contacts);
      }
    });
  }

  selectContact(contact: Contact) {
    console.log('Clicked contact:', contact);

    this.selectedContacts.push(contact);
    this.selectedContactsSubject.next(this.selectedContacts);
  }

  unselectContact(contact: Contact) {
    console.log('Unselected contact:', contact);

    // remove contact
    this.selectedContacts = this.selectedContacts.filter((c) => c !== contact);
    this.selectedContactsSubject.next(this.selectedContacts);
  }

  createGroup() {
    let userIds = this.selectedContacts.map((contact) => contact.id.toString());
    userIds.push(this.storageService.getUser().userId);

    let channelCreationDto: ChannelCreationDto = {
      channelName: this.groupNameInput.nativeElement.value,
      chatUsersIds: userIds
    };

    this.channelService.addChannel(channelCreationDto).subscribe({
      next: (response) => {
        console.log(response.message); // Assuming response is { message: "Success message" }
        this.router.navigate(['apps/chat']);
      },
      error: (err) => {
        console.error(err.error); // Assuming error response is { error: "Error message" }
      }
    });
  }
}
