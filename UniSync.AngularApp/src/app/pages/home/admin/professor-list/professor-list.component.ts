import { Component, OnInit } from '@angular/core';
import { scaleIn400ms } from '@vex/animations/scale-in.animation';
import { fadeInRight400ms } from '@vex/animations/fade-in-right.animation';
import { TableColumn } from '@vex/interfaces/table-column.interface';
import { ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { debounceTime } from 'rxjs/operators';
import { stagger40ms } from '@vex/animations/stagger.animation';
import { MatDialog } from '@angular/material/dialog';
import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';

import { StudentService } from 'src/app/_services/student.service';
import { UserService } from 'src/app/_services/user.service';
import { ProfessorService } from 'src/app/_services/professor.service';

export interface Contact {
  id: number;
  imageSrc: string;
  name: string;
  email: string;
  role: string;
}

@Component({
  selector: 'student-list-table',
  templateUrl: './professor-list.component.html',
  animations: [stagger40ms, scaleIn400ms, fadeInRight400ms],
  styles: [
    `
      .mat-drawer-container {
        background: transparent !important;
      }
    `
  ],
  standalone: true,
  imports: [
    MatButtonModule,
    MatIconModule,
    ReactiveFormsModule,
    MatSidenavModule,
    AsyncPipe,
    NgFor,
    NgIf
  ]
})
export class ProfessorListComponent implements OnInit {
  activeUser: Contact | null = null;
  isDropdownOpen = false;

  users: Contact[] = [];
  searchCtrl = new UntypedFormControl();

  searchStr$ = this.searchCtrl.valueChanges.pipe(debounceTime(10));

  menuOpen = false;

  constructor(
    private dialog: MatDialog,
    private userService: UserService,
    private router: Router,
    private studentService: StudentService,
    private changeDetectorRef: ChangeDetectorRef,
    private professorService: ProfessorService
  ) {}

  ngOnInit() {
    this.getAllStudents();
  }

  openContact(id?: Contact['id']) {
    console.log('Clicked contact!');
    this.router.navigate(['apps/evaluation/' + id]);
  }

  toggleStar(id: Contact['id']) {
    const contact = this.users.find((c) => c.id === id);

    // if (contact) {
    //   contact.starred = !contact.starred;
    // }
  }

  setData(data: Contact[]) {
    this.users = data;
    this.menuOpen = false;
  }

  openMenu() {
    this.menuOpen = true;
  }
  onEnterPressed(request: string) {
    this.professorService.searchProfessors(request).subscribe({
      next: (students) => {
        console.log(students);

        let searchedContacts: Contact[] = [];

        for (const searchedUser of students) {
          let searchedContact: Contact = {
            id: 1,
            imageSrc: 'assets/img/avatars/1.jpg',
            name: searchedUser.firstName + ' ' + searchedUser.lastName,
            role: 'student',
            email: searchedUser.email
          };

          searchedContacts.push(searchedContact);
        }

        this.users = searchedContacts;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getAllStudents() {
    this.professorService.getAllProfessors().subscribe({
      next: (students) => {
        let retreivedStudents: Contact[] = [];

        students.forEach((s) => {
          let student: Contact = {
            id: s.userId,
            imageSrc:
              'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRXJA32WU4rBpx7maglqeEtt3ot1tPIRWptxA&s',
            name: `${s.firstName} ${s.lastName}`,
            email: s.email,
            role: 'Student'
          };

          retreivedStudents.push(student);
        });

        this.users = retreivedStudents;
        //this.changeDetectorRef.detectChanges();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  toggleDropdown(user: Contact): void {
    if (this.activeUser === user && this.isDropdownOpen) {
      this.isDropdownOpen = false;
    } else {
      this.activeUser = user;
      this.isDropdownOpen = true;
    }
  }

  removeUser(user: Contact): void {
    // Implement your remove user logic here
    console.log(`Removing user: ${user.name}`);
    // Example of removing user from the list
    this.users = this.users.filter((u) => u !== user);
    this.isDropdownOpen = false; // Close dropdown after action
  }
}
