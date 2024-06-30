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
import { CourseService } from 'src/app/_services/course.service';

export interface Course {
  id: number;
  name: string;
  credits: string;
  semester: string;
}

@Component({
  selector: 'student-list-table',
  templateUrl: './course-list.component.html',
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
export class CourseListComponent implements OnInit {
  activeUser: Course | null = null;
  isDropdownOpen = false;

  courses: Course[] = [];
  searchCtrl = new UntypedFormControl();

  searchStr$ = this.searchCtrl.valueChanges.pipe(debounceTime(10));

  menuOpen = false;

  constructor(
    private dialog: MatDialog,
    private userService: UserService,
    private router: Router,
    private studentService: StudentService,
    private changeDetectorRef: ChangeDetectorRef,
    private courseService: CourseService
  ) {}

  ngOnInit() {
    this.getAllStudents();
  }

  openContact(id?: Course['id']) {
    console.log('Clicked contact!');
    this.router.navigate(['apps/evaluation/' + id]);
  }

  toggleStar(id: Course['id']) {
    const contact = this.courses.find((c) => c.id === id);

    // if (contact) {
    //   contact.starred = !contact.starred;
    // }
  }

  setData(data: Course[]) {
    this.courses = data;
    this.menuOpen = false;
  }

  openMenu() {
    this.menuOpen = true;
  }
  onEnterPressed(request: string) {
    this.courseService.getAllCourses().subscribe({
      next: (courses) => {
        console.log(courses);

        let searchedCourses: Course[] = [];

        for (const searchedCourse of courses) {
          let searchedContact: Course = {
            id: 1,
            name: searchedCourse.courseName,
            credits: searchedCourse.credits,
            semester: searchedCourse.semester
          };

          searchedCourses.push(searchedContact);
        }

        this.courses = searchedCourses;
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getAllStudents() {
    this.courseService.getAllCourses().subscribe({
      next: (courses) => {
        console.log(courses);
        let retreivedCourses: Course[] = [];

        courses.forEach((s) => {
          let coursee: Course = {
            id: 1,
            name: s.courseName,
            credits: s.credits,
            semester: s.semester
          };

          retreivedCourses.push(coursee);
        });

        this.courses = retreivedCourses;
        this.changeDetectorRef.detectChanges();
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
  toggleDropdown(user: Course): void {
    if (this.activeUser === user && this.isDropdownOpen) {
      this.isDropdownOpen = false;
    } else {
      this.activeUser = user;
      this.isDropdownOpen = true;
    }
  }

  removeUser(user: Course): void {
    // Implement your remove user logic here
    console.log(`Removing user: ${user.name}`);
    // Example of removing user from the list
    this.courses = this.courses.filter((u) => u !== user);
    this.isDropdownOpen = false; // Close dropdown after action
  }
}
