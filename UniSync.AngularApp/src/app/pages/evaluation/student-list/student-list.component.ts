import { Component, OnInit } from '@angular/core';
import { scaleIn400ms } from '@vex/animations/scale-in.animation';
import { fadeInRight400ms } from '@vex/animations/fade-in-right.animation';
import { TableColumn } from '@vex/interfaces/table-column.interface';
import { contactsData } from '../../../../static-data/contacts';
import { ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { debounceTime } from 'rxjs/operators';
import { stagger40ms } from '@vex/animations/stagger.animation';
import { MatDialog } from '@angular/material/dialog';
import { AsyncPipe } from '@angular/common';
import { ContactsDataTableComponent } from '../../contacts/contacts-table/contacts-data-table/contacts-data-table.component';
import { ContactsTableMenuComponent } from '../../contacts/contacts-table/contacts-table-menu/contacts-table-menu.component';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { UserService } from '../../../_services/user.service';
import { Router } from '@angular/router';
import { StudentService } from '../../../_services/student.service';
import { ChangeDetectorRef } from '@angular/core';

export interface Contact {
  id: number;
  imageSrc: string;
  name: string;
  email: string;
  role: string;
  phone?: string;
  bio?: string;
  birthday?: string;
  selected: boolean;
}

@Component({
  selector: 'student-list-table',
  templateUrl: './student-list.component.html',
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
    ContactsTableMenuComponent,
    ContactsDataTableComponent,
    AsyncPipe
  ]
})
export class StudentListComponent implements OnInit {
  searchCtrl = new UntypedFormControl();

  searchStr$ = this.searchCtrl.valueChanges.pipe(debounceTime(10));

  menuOpen = false;

  activeCategory:
    | 'frequently'
    | 'starred'
    | 'all'
    | 'family'
    | 'friends'
    | 'colleagues'
    | 'business' = 'all';
  // tableData = contactsData;

  tableData: Contact[] = [];

  tableColumns: TableColumn<Contact>[] = [
    {
      label: '',
      property: 'selected',
      type: 'checkbox',
      cssClasses: ['w-6']
    },
    {
      label: '',
      property: 'imageSrc',
      type: 'image',
      cssClasses: ['min-w-9']
    },
    {
      label: 'NAME',
      property: 'name',
      type: 'text',
      cssClasses: ['font-medium']
    },
    {
      label: 'EMAIL',
      property: 'email',
      type: 'text',
      cssClasses: ['text-secondary']
    },
    {
      label: 'ROLE',
      property: 'role',
      type: 'text',
      cssClasses: ['text-secondary']
    },
    {
      label: '',
      property: 'starred',
      type: 'button',
      cssClasses: ['text-secondary', 'w-10']
    },
    {
      label: '',
      property: 'menu',
      type: 'button',
      cssClasses: ['text-secondary', 'w-10']
    }
  ];

  constructor(
    private dialog: MatDialog,
    private userService: UserService,
    private router: Router,
    private studentService: StudentService,
    private changeDetectorRef: ChangeDetectorRef
  ) {}

  ngOnInit() {
    this.getAllStudents();
  }

  openContact(id?: Contact['id']) {
    console.log('Clicked contact!');
    this.router.navigate(['apps/evaluation/' + id]);
  }

  toggleStar(id: Contact['id']) {
    const contact = this.tableData.find((c) => c.id === id);

    // if (contact) {
    //   contact.starred = !contact.starred;
    // }
  }

  setData(data: Contact[]) {
    this.tableData = data;
    this.menuOpen = false;
  }

  openMenu() {
    this.menuOpen = true;
  }
  onEnterPressed(request: string) {
    this.studentService.searchStudents(request).subscribe({
      next: (students) => {
        console.log(students);

        let searchedContacts: Contact[] = [];

        for (const searchedUser of students) {
          let searchedContact: Contact = {
            id: 1,
            imageSrc: 'assets/img/avatars/1.jpg',
            name: searchedUser.firstName + ' ' + searchedUser.lastName,
            role: 'student',
            email: searchedUser.email,
            selected: false
          };

          searchedContacts.push(searchedContact);
        }

        this.tableData = searchedContacts;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getAllStudents() {
    this.studentService.getAllStudents().subscribe({
      next: (students) => {
        let retreivedStudents: Contact[] = [];

        students.forEach((s) => {
          let student: Contact = {
            id: s.userId,
            imageSrc:
              'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRXJA32WU4rBpx7maglqeEtt3ot1tPIRWptxA&s',
            name: `${s.firstName} ${s.lastName}`,
            email: s.email,
            role: 'Student',
            selected: false
          };

          retreivedStudents.push(student);
        });

        this.tableData = retreivedStudents;
        //this.changeDetectorRef.detectChanges();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}
