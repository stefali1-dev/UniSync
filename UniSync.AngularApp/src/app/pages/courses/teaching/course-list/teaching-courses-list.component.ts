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
