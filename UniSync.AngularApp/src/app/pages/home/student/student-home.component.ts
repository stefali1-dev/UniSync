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
import { UserService } from 'src/app/_services/user.service';
import { Chat } from '../../chat/chat.component';
import { recentChats } from 'src/static-data/mock';

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
  recentChats: Chat[] = []; // Vom păstra acest lucru ca mock data pentru moment

  constructor(
    private storageService: StorageService,
    private courseService: CourseService,
    private timetableService: TimetableService,
    private evaluationService: EvaluationService,
    private changeDetectorRef: ChangeDetectorRef,
    private studentService: StudentService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    //this.loadStudentData();

    let userId = this.storageService.getUser().userId;

    if (userId === '7fef5c57-29de-4852-9729-979475c08417') {
      this.studentName = 'Michael';
      this.dailySchedule = [
        {
          TimeInterval: '09:00 - 10:30',
          CourseId: 'CS201',
          CourseName: 'Data Structures',
          CourseType: 'Lecture',
          ProfessorId: 'P004',
          ProfessorName: 'Dr. Sarah Johnson',
          Classroom: 'CS102',
          DayOfWeek: 2,
          StudentGroup: 'CS2A'
        },
        {
          TimeInterval: '11:00 - 12:30',
          CourseId: 'CS305',
          CourseName: 'Databases',
          CourseType: 'Lab',
          ProfessorId: 'P005',
          ProfessorName: 'Dr. Michael Chen',
          Classroom: 'LAB201',
          DayOfWeek: 2,
          StudentGroup: 'CS3B'
        },
        {
          TimeInterval: '14:00 - 15:30',
          CourseId: 'CS401',
          CourseName: 'Artificial Intelligence',
          CourseType: 'Seminar',
          ProfessorId: 'P006',
          ProfessorName: 'Dr. Elena Rodriguez',
          Classroom: 'CS305',
          DayOfWeek: 2,
          StudentGroup: 'CS4A'
        }
      ];

      this.enrolledCourses = [
        {
          courseId: 'CS201',
          courseName: 'Data Structures',
          courseNumber: 'CS201-01',
          credits: '4',
          description:
            'An in-depth study of data structures and algorithms with a focus on efficiency and optimization.',
          semester: 'Spring 2025',
          evaluations: [
            {
              courseId: 'CS201',
              type: 'Project',
              courseName: 'Data Structures',
              grade: 92,
              dateTime: new Date('2025-04-10T15:00:00'),
              comment: 'Excellent implementation of advanced data structures.'
            }
          ]
        },
        {
          courseId: 'CS305',
          courseName: 'Databases',
          courseNumber: 'CS305-02',
          credits: '3',
          description:
            'Design and implementation of database systems, including SQL and NoSQL databases.',
          semester: 'Spring 2025',
          evaluations: []
        },
        {
          courseId: 'CS401',
          courseName: 'Artificial Intelligence',
          courseNumber: 'CS401-01',
          credits: '4',
          description:
            'Introduction to AI concepts, machine learning algorithms, and their applications.',
          semester: 'Spring 2025',
          evaluations: []
        }
      ];

      this.studentEvaluations = [
        {
          courseId: 'CS201',
          type: 'Project',
          courseName: 'Data Structures',
          grade: 9,
          dateTime: new Date('2025-04-10T15:00:00'),
          comment: 'Excellent implementation of advanced data structures.'
        },
        {
          courseId: 'CS305',
          type: 'Midterm',
          courseName: 'Databases',
          grade: 8,
          dateTime: new Date('2025-03-20T10:00:00')
        },
        {
          courseId: 'CS401',
          type: 'Research Paper',
          courseName: 'Artificial Intelligence',
          grade: 10,
          dateTime: new Date('2025-05-15T14:00:00'),
          comment: 'Outstanding research on emerging AI technologies.'
        }
      ];

      this.recentChats = [
        {
          id: '1',
          imageUrl:
            'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcROnvZt4c3u_yXi7p_eUslwwZvj9yZk0L6hyw&s',
          name: 'Group 1A2',
          lastMessage: 'Anyone want to review heap sort implementation?',
          unreadCount: 2,
          timestamp: '3 hours ago',
          nrOfParticipants: 8
        },
        {
          id: '2',
          imageUrl: 'https://i.pravatar.cc/150?img=45',
          name: 'Dr. Sarah Johnson',
          lastMessage: 'Office hours extended to 5 PM this Thursday.',
          unreadCount: 1,
          timestamp: '1 day ago',
          nrOfParticipants: 2
        },
        {
          id: '3',
          imageUrl:
            'https://cdn.britannica.com/85/13085-050-C2E88389/Corpus-Christi-College-University-of-Cambridge-England.jpg',
          name: 'Year 1',
          lastMessage: 'Has anyone started working on the project yet?',
          unreadCount: 0,
          timestamp: '3 days ago',
          nrOfParticipants: 30
        }
      ];
    } else {
      this.studentName = 'John';
      this.dailySchedule = [
        {
          TimeInterval: '08:00 - 09:30',
          CourseId: 'CS101',
          CourseName: 'Introduction to Programming',
          CourseType: 'Lecture',
          ProfessorId: 'P001',
          ProfessorName: 'Dr. John Smith',
          Classroom: 'A101',
          DayOfWeek: 1,
          StudentGroup: 'CS1A'
        },
        {
          TimeInterval: '10:00 - 11:30',
          CourseId: 'MA201',
          CourseName: 'Linear Algebra',
          CourseType: 'Seminar',
          ProfessorId: 'P002',
          ProfessorName: 'Dr. Emily Johnson',
          Classroom: 'B205',
          DayOfWeek: 1,
          StudentGroup: 'MA2B'
        },
        {
          TimeInterval: '13:00 - 14:30',
          CourseId: 'PH301',
          CourseName: 'Quantum Mechanics',
          CourseType: 'Laboratory',
          ProfessorId: 'P003',
          ProfessorName: 'Dr. Michael Brown',
          Classroom: 'C303',
          DayOfWeek: 1,
          StudentGroup: 'PH3A'
        }
      ];

      this.enrolledCourses = [
        {
          courseId: 'CS101',
          courseName: 'Introduction to Programming',
          courseNumber: 'CS101-01',
          credits: '4',
          description:
            'An introductory course to programming concepts and practices.',
          semester: 'Fall 2024',
          evaluations: [
            {
              courseId: 'CS101',
              type: 'Midterm',
              courseName: 'Introduction to Programming',
              grade: 85,
              dateTime: new Date('2024-10-15T14:00:00'),
              comment: 'Good understanding of basic concepts.'
            }
          ]
        },
        {
          courseId: 'MA201',
          courseName: 'Linear Algebra',
          courseNumber: 'MA201-02',
          credits: '3',
          description:
            'A study of linear equations, matrices, and vector spaces.',
          semester: 'Fall 2024',
          evaluations: []
        },
        {
          courseId: 'PH301',
          courseName: 'Quantum Mechanics',
          courseNumber: 'PH301-01',
          credits: '4',
          description:
            'An advanced course on quantum theory and its applications.',
          semester: 'Fall 2024',
          evaluations: []
        }
      ];

      this.studentEvaluations = [
        {
          courseId: 'CS101',
          type: 'Midterm',
          courseName: 'Introduction to Programming',
          grade: 10,
          dateTime: new Date('2024-10-15T14:00:00'),
          comment: 'Good understanding of basic concepts.'
        },
        {
          courseId: 'MA201',
          type: 'Quiz',
          courseName: 'Linear Algebra',
          grade: 7,
          dateTime: new Date('2024-09-30T10:30:00')
        },
        {
          courseId: 'PH301',
          type: 'Lab Report',
          courseName: 'Quantum Mechanics',
          grade: 4,
          dateTime: new Date('2024-11-05T16:45:00'),
          comment: 'Well-structured report with minor calculation errors.'
        }
      ];

      this.recentChats = recentChats;
    }
  }

  loadStudentData(): void {
    const user = this.storageService.getUser();
    if (user.role !== 'Student') {
      console.log('User is not a student!');
      return;
    }

    this.userService.getUserById(user.userId).subscribe({
      next: (data) => {
        this.studentName = data.user.firstName;
      }
    });

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
