import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { StorageService } from 'src/app/_services/storage.service';
import { TimetableService } from 'src/app/_services/timetable.service';
import { NgFor } from '@angular/common';

export interface TimetableEntry {
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
  selector: 'app-timetable',
  standalone: true,
  imports: [NgFor],
  templateUrl: './timetable-professor.component.html'
})
export class TimetableComponent implements OnInit {
  timetableEntries: TimetableEntry[] = [];
  daysWithEntries: string[] = [];

  dayOfWeek: string[] = [
    'Monday',
    'Tuesday',
    'Wednesday',
    'Thursday',
    'Friday',
    'Saturday',
    'Sunday'
  ];

  constructor(
    private storageService: StorageService,
    private timetableService: TimetableService
  ) {}

  ngOnInit(): void {
    this.getTimetableEntries();
  }

  getTimetableEntries() {
    let user = this.storageService.getUser();

    if (user.role !== 'Professor') {
      console.log('User not a professor!');
      return;
    }

    this.timetableService
      .getTimetableEntriesByProfessorId(user.userId)
      .subscribe({
        next: (timetableEntries) => {
          let receivedEntries: TimetableEntry[] = [];

          timetableEntries.forEach((te) => {
            let timetableEntry: TimetableEntry = {
              TimeInterval: te.timeInterval,
              CourseId: te.courseId,
              CourseName: te.courseName,
              CourseType: te.courseType,
              ProfessorId: te.professorId,
              ProfessorName: te.professorName,
              Classroom: te.classroom,
              DayOfWeek: te.dayOfWeek,
              StudentGroup: te.studentGroup
            };

            receivedEntries.push(timetableEntry);
          });

          this.timetableEntries = receivedEntries;
          this.daysWithEntries = Array.from(
            new Set(
              this.timetableEntries.map(
                (entry) => this.dayOfWeek[entry.DayOfWeek]
              )
            )
          );
        },
        error: (err) => {
          console.log(err);
        }
      });
  }
  getEntriesForDay(day: string): TimetableEntry[] {
    const dayIndex = this.dayOfWeek.indexOf(day);
    return this.timetableEntries.filter(
      (entry) => entry.DayOfWeek === dayIndex
    );
  }
}
