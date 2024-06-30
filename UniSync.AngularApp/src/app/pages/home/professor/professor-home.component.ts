import { Component, OnInit } from '@angular/core';
// import { EvaluationView } from '../../evaluation/student-page/student-page.component';
// import { CourseView } from '../../evaluation/student-page/student-page.component';
// import { TimetableEntry } from '../../timetable/timetable-professor/timetable-professor.component';
import { RouterLink } from '@angular/router';
import { NgFor } from '@angular/common';

interface EvaluationView {
  courseId: string;
  courseName: string;
  grade: number;
  dateTime: Date;
  comment?: string;
  studentId: string;
  studentName: string;
}

interface CourseView {
  courseId: string;
  courseName: string;
  courseNumber: string;
  credits: string;
  description: string;
  semester: string;
}

interface TimetableEntry {
  TimeInterval: string;
  CourseId: string;
  CourseName: string;
  CourseType: string;
  ProfessorId: string;
  ProfessorName: string;
  Classroom: string;
  DayOfWeek: number;
  StudentGroup: string;
}

@Component({
  selector: 'app-teacher-homepage',
  standalone: true,
  imports: [RouterLink, NgFor],
  templateUrl: './professor-home.component.html'
})
export class ProfessorHomeComponent implements OnInit {
  teacherName: string = 'Prof. Smith';
  dailySchedule: TimetableEntry[] = [];
  taughtCourses: CourseView[] = [];
  pendingEvaluations: EvaluationView[] = [];
  recentChats: any[] = [];

  constructor() {}

  ngOnInit(): void {
    this.loadMockData();
  }

  loadMockData(): void {
    // Mock data pentru orarul zilei
    this.dailySchedule = [
      {
        TimeInterval: '10:00-12:00',
        CourseId: '1',
        CourseName: 'Matematică',
        CourseType: 'Curs',
        ProfessorId: 'P1',
        ProfessorName: this.teacherName,
        Classroom: 'C101',
        DayOfWeek: 1,
        StudentGroup: 'A1'
      },
      {
        TimeInterval: '14:00-16:00',
        CourseId: '2',
        CourseName: 'Algebră',
        CourseType: 'Seminar',
        ProfessorId: 'P1',
        ProfessorName: this.teacherName,
        Classroom: 'S201',
        DayOfWeek: 1,
        StudentGroup: 'B2'
      }
    ];

    // Mock data pentru cursurile predate
    this.taughtCourses = [
      {
        courseId: '1',
        courseName: 'Matematică',
        courseNumber: 'MATH101',
        credits: '5',
        description: 'Curs de matematică',
        semester: '1'
      },
      {
        courseId: '2',
        courseName: 'Algebră',
        courseNumber: 'MATH201',
        credits: '6',
        description: 'Curs de algebră',
        semester: '1'
      },
      {
        courseId: '3',
        courseName: 'Analiză',
        courseNumber: 'MATH301',
        credits: '5',
        description: 'Curs de analiză',
        semester: '2'
      }
    ];

    // Mock data pentru evaluările în așteptare
    this.pendingEvaluations = [
      {
        courseId: '1',
        courseName: 'Matematică',
        grade: 0,
        dateTime: new Date(),
        studentId: 'S1',
        studentName: 'John Doe'
      },
      {
        courseId: '1',
        courseName: 'Matematică',
        grade: 0,
        dateTime: new Date(),
        studentId: 'S2',
        studentName: 'Jane Smith'
      },
      {
        courseId: '2',
        courseName: 'Algebră',
        grade: 0,
        dateTime: new Date(),
        studentId: 'S3',
        studentName: 'Bob Johnson'
      }
    ];

    // Mock data pentru chat-uri recente
    this.recentChats = [
      {
        name: 'John Doe',
        avatar: 'assets/avatar1.jpg',
        lastMessage: new Date('2023-06-28T15:30:00')
      },
      {
        name: 'Jane Smith',
        avatar: 'assets/avatar2.jpg',
        lastMessage: new Date('2023-06-28T18:45:00')
      }
    ];
  }

  formatTime(date: Date): string {
    return date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
  }
}
