import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CourseCardComponent } from '../../card/course-card.component';
import { OnInit } from '@angular/core';
import { Course } from '../../enrolled/course-list/enrolled-courses-list.component';
import { CourseService } from '../../../../_services/course.service';
import { StorageService } from '../../../../_services/storage.service';
import { ChangeDetectorRef } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [CommonModule, CourseCardComponent, RouterLink],
  templateUrl: './teaching-courses-list.component.html'
})
export class TeachingCourseListComponent implements OnInit {
  public courses!: Course[];

  // public courses: Course[] = [
  //   {
  //     courseId: 'CS101',
  //     courseName: 'Data Structures',
  //     courseNumber: '101',
  //     credits: '6',
  //     description: 'Learn about data structures and algorithms.',
  //     semester: '1'
  //   },
  //   {
  //     courseId: 'CS201',
  //     courseName: 'Object Oriented Programming',
  //     courseNumber: '201',
  //     credits: '6',
  //     description: 'Explore OOP principles and design patterns.',
  //     semester: '2'
  //   },
  //   {
  //     courseId: 'CS401',
  //     courseName: 'Databases',
  //     courseNumber: '401',
  //     credits: '6',
  //     description: 'Learn about relational databases and SQL.',
  //     semester: '3'
  //   },
  //   {
  //     courseId: 'CS6021',
  //     courseName: 'Advanced Programming',
  //     courseNumber: '621',
  //     credits: '5',
  //     description: 'Advanced topics in the Java programming language.',
  //     semester: '4'
  //   },
  //   {
  //     courseId: 'CS501',
  //     courseName: 'Machine Learning',
  //     courseNumber: '501',
  //     credits: '6',
  //     description: 'Explore ML algorithms and model training.',
  //     semester: '5'
  //   }
  // ];

  constructor(
    private courseService: CourseService,
    private storageService: StorageService,
    private changeDetectorRef: ChangeDetectorRef
  ) {}
  ngOnInit(): void {
    this.getEnrolledCourses();
  }

  getEnrolledCourses() {
    let user = this.storageService.getUser();

    if (user.role === 'Professor') {
      this.courseService.getCoursesByProfessorId(user.appUserId).subscribe({
        next: (courses) => {
          console.log(courses);
          let coursesArray: Course[] = [];

          courses.forEach((c) => {
            let course: Course = {
              courseId: c.courseId,
              courseName: c.courseName,
              courseNumber: c.courseNumber,
              credits: c.credits,
              description: c.description,
              semester: c.semester
            };

            coursesArray.push(course);
          });

          this.courses = coursesArray;
          this.changeDetectorRef.detectChanges();
        },
        error: (err) => {
          console.log(err);
        }
      });
    } else {
      console.log('The user is not a professor!');
    }
  }
}
