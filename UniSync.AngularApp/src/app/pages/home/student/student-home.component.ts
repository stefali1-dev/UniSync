import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { NgClass, NgFor } from '@angular/common';
import { RouterLink } from '@angular/router';
// import { StorageService } from '../../../../_services/storage.service';
// import { CourseService } from '../../../../_services/course.service';
// import { TimetableService } from '../../../../_services/timetable.service';
// import { EvaluationService } from '../../../../_services/evaluation.service';
import { StorageService } from 'src/app/_services/storage.service';
import { CourseService } from 'src/app/_services/course.service';
import { TimetableService } from 'src/app/_services/timetable.service';
import { EvaluationService } from 'src/app/_services/evaluation.service';
import { TimetableEntry } from '../../timetable/timetable-professor/timetable-professor.component';
import { CourseView } from '../../evaluation/student-page/student-page.component';
import { EvaluationView } from '../../evaluation/student-page/student-page.component';
import { StudentService } from 'src/app/_services/student.service';

@Component({
  selector: 'app-student-homepage',
  standalone: true,
  imports: [NgClass, NgFor, RouterLink],
  templateUrl: './student-home.component.html'
})
export class StudentHomeComponent implements OnInit {
  studentName: string = '';
  dailySchedule: TimetableEntry[] = [];
  enrolledCourses: CourseView[] = [];
  studentEvaluations: EvaluationView[] = [];
  recentChats: any[] = []; // Vom păstra acest lucru ca mock data pentru moment

  constructor(
    private storageService: StorageService,
    private courseService: CourseService,
    private timetableService: TimetableService,
    private evaluationService: EvaluationService,
    private changeDetectorRef: ChangeDetectorRef,
    private studentService: StudentService
  ) {}

  ngOnInit(): void {
    this.loadStudentData();
  }

  loadStudentData(): void {
    const user = this.storageService.getUser();
    if (user.role !== 'Student') {
      console.log('User is not a student!');
      return;
    }

    this.studentName = user.username; // Presupunem că username este disponibil în obiectul user

    this.getEnrolledCourses(user.appUserId);

    this.studentService.getStudentByChatUserId(user.userId).subscribe({
      next: (student) => {
        this.getDailySchedule(student.group);
        this.getStudentEvaluations(user.appUserId);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  getEnrolledCourses(studentId: string): void {
    this.courseService.getCoursesByStudentId(studentId).subscribe({
      next: (courses) => {
        this.enrolledCourses = courses.map((c) => ({
          courseId: c.courseId,
          courseName: c.courseName,
          courseNumber: c.courseNumber,
          credits: c.credits,
          description: c.description,
          semester: c.semester
        }));

        console.log(this.enrolledCourses);
        this.changeDetectorRef.detectChanges();
      },
      error: (err) => console.error('Error fetching enrolled courses:', err)
    });
  }

  getDailySchedule(groupName: string): void {
    const today = new Date().getDay(); // 0 pentru Duminică, 1 pentru Luni, etc.
    this.timetableService
      .getTimetableEntriesByStudentGroupName(groupName)
      .subscribe({
        next: (entries) => {
          console.log(entries);

          this.dailySchedule = entries
            .filter((entry) => entry.DayOfWeek === today)
            .map((entry) => ({
              TimeInterval: entry.TimeInterval,
              CourseId: entry.CourseId,
              CourseName: entry.CourseName,
              CourseType: entry.CourseType,
              ProfessorId: entry.ProfessorId,
              ProfessorName: entry.ProfessorName,
              Classroom: entry.Classroom,
              DayOfWeek: entry.DayOfWeek,
              StudentGroup: entry.StudentGroup
            }));
          this.changeDetectorRef.detectChanges();
        },
        error: (err) => console.error('Error fetching daily schedule:', err)
      });
  }

  getStudentEvaluations(studentId: string): void {
    this.evaluationService.getEvaluationsByStudentId(studentId).subscribe({
      next: (evaluations) => {
        this.studentEvaluations = evaluations.map((e) => ({
          courseId: e.courseId,
          courseName: e.courseName,
          grade: e.grade,
          dateTime: new Date(e.dateTime),
          comment: e.comment
        }));
        this.changeDetectorRef.detectChanges();
      },
      error: (err) => console.error('Error fetching student evaluations:', err)
    });
  }

  formatTime(date: Date): string {
    return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
  }
}
