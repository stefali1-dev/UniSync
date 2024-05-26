import {
    Component, OnInit  } from '@angular/core';
  import { VexScrollbarComponent } from '@vex/components/vex-scrollbar/vex-scrollbar.component';
  import { AsyncPipe, NgFor } from '@angular/common';
  import { CommonModule } from '@angular/common';
  
  import { MatButtonModule } from '@angular/material/button';
  import { MatInputModule } from '@angular/material/input';
  import { UserService } from '../../../_services/user.service';
  import {MatDialogModule} from '@angular/material/dialog';
  import { BehaviorSubject } from 'rxjs';
  import {ChatService} from '../chat.service'
  import {StorageService} from '../../../_services/storage.service'



export interface Contact {
    id: number;
    imageSrc: string;
    name: string;
    email: string;
    role: string;
    selected: boolean;
  }
  
  @Component({
    selector: 'dialog-user-list',
    templateUrl: 'dialog-user-list.html',
    standalone: true,
    imports: [
      MatDialogModule,
       MatButtonModule,
       VexScrollbarComponent,
       MatInputModule,
       NgFor,
       AsyncPipe,
       CommonModule
      ],
  })
  export class DialogUserList implements OnInit {
  
    contacts: Contact[] = [];
  
    private contactsSubject = new BehaviorSubject<Contact[]>(this.contacts);
    public contacts$ = this.contactsSubject.asObservable();
  
    constructor(
      private userService: UserService,
      private chatService: ChatService,
      private storageService: StorageService
    ) {}

    ngOnInit() {
        this.getInitialContacts(10)
      }
  
    onEnterPressed(request: string) {
      this.userService.getSearchedUsers(request).subscribe({
        next: data => {
          console.log(data.users);
  
          let fetchedUsers: Contact[] = [];
  
          data.users.forEach(user => {
  
            let contact: Contact = {
              id: user.userId,
              imageSrc: user.userPhoto,
              name: user.firstName + ' ' + user.lastMessage,
              email: user.email,
              role: 'student',
              selected: false
            }
  
            fetchedUsers.push(contact)
          });
  
          this.contacts = fetchedUsers;
          this.contactsSubject.next(this.contacts);
  
        },
        error: err => {
          console.log(err);
        }
      });
    }
  
    getInitialContacts(numberOfContacts: number){
  
      this.userService.getAllUsers().subscribe({
        next: data => {
  
          let fetchedUsers: Contact[] = [];
  
          data.users.slice(0, numberOfContacts).forEach((user) => {
            
            let contact: Contact = {
              id: user.userId,
              imageSrc: user.userPhoto,
              name: user.firstName + ' ' + user.lastMessage,
              email: user.email,
              role: 'student',
              selected: false
            }
  
            fetchedUsers.push(contact)
          });
  
          this.contacts = fetchedUsers;
          this.contactsSubject.next(this.contacts);
  
        }
      });
    }

    startConversation(contact: Contact) {
        console.log('Clicked contact:', contact);
        
        let userIds: string[] = [this.storageService.getUser().userId, contact.id]

        this.chatService.createChat('DM', userIds);
      }
  }
  