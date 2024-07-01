import { Component, OnInit } from '@angular/core';
import { CommonModule, NgIf } from '@angular/common';
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
  imports: [NgFor, NgIf],
  templateUrl: './timetable-student.component.html'
})
export class TimetableStudentComponent implements OnInit {
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

    // this.timetableEntries = [
    //   {
    //     TimeInterval: '08-10',
    //     CourseId: 'PS001',
    //     CourseName: 'Probabilitati si statistica',
    //     CourseType: 'Curs',
    //     ProfessorId: 'OFE001',
    //     ProfessorName: 'Lect. dr. Olariu Florentin Emanuel',
    //     Classroom: 'C112',
    //     DayOfWeek: 1,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '10-12',
    //     CourseId: 'FAI001',
    //     CourseName: 'Fundamentele Algebrice ale Informaticii',
    //     CourseType: 'Curs',
    //     ProfessorId: 'TFL001',
    //     ProfessorName: 'Prof. dr. Țiplea Ferucio Laurențiu',
    //     Classroom: 'C2',
    //     DayOfWeek: 1,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '16-18',
    //     CourseId: 'FAI002',
    //     CourseName: 'Fundamentele Algebrice ale Informaticii',
    //     CourseType: 'Seminar',
    //     ProfessorId: 'HM001',
    //     ProfessorName: 'Colab. Horduna Manuela',
    //     Classroom: 'C905',
    //     DayOfWeek: 1,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '18-20',
    //     CourseId: 'PED001',
    //     CourseName: 'Pedagogie 1',
    //     CourseType: 'Seminar',
    //     ProfessorId: 'MLA001',
    //     ProfessorName: 'CS dr. Magurianu Liviu Adrian',
    //     Classroom: 'C905',
    //     DayOfWeek: 1,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '18-20',
    //     CourseId: 'POO001',
    //     CourseName: 'Programare orientata-obiect',
    //     CourseType: 'Laborator',
    //     ProfessorId: 'SAA001',
    //     ProfessorName: 'Colab. Stoica Alin Alexandru',
    //     Classroom: 'C411',
    //     DayOfWeek: 1,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '08-10',
    //     CourseId: 'PED002',
    //     CourseName: 'Pedagogie 1',
    //     CourseType: 'Curs',
    //     ProfessorId: 'MLA001',
    //     ProfessorName: 'CS dr. Magurianu Liviu Adrian',
    //     Classroom: 'C2',
    //     DayOfWeek: 2,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '08-10',
    //     CourseId: 'PS002',
    //     CourseName: 'Probabilitati si statistica',
    //     CourseType: 'Laborator',
    //     ProfessorId: 'OFE001',
    //     ProfessorName: 'Lect. dr. Olariu Florentin Emanuel',
    //     Classroom: 'C411',
    //     DayOfWeek: 2,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '12-14',
    //     CourseId: 'SO001',
    //     CourseName: 'Sisteme de Operare',
    //     CourseType: 'Curs',
    //     ProfessorId: 'VC001',
    //     ProfessorName: 'Lect. dr. Vidrașcu Cristian',
    //     Classroom: 'C2',
    //     DayOfWeek: 2,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '16-17',
    //     CourseId: 'EF001',
    //     CourseName: 'Educatie Fizica',
    //     CourseType: 'Practic',
    //     ProfessorId: 'PML001',
    //     ProfessorName: 'Colab. Paraschiv Marius-Lucian',
    //     Classroom: 'Teren Sport',
    //     DayOfWeek: 2,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '16-18',
    //     CourseId: 'PA001',
    //     CourseName: 'Proiectarea Algoritmilor',
    //     CourseType: 'Curs',
    //     ProfessorId: 'CS001,DP001,LD001',
    //     ProfessorName:
    //       'Conf. dr. Ciobâcă Ștefan, Lect. dr. Diac Paul, Prof. dr. Lucanu Dorel',
    //     Classroom: 'C2',
    //     DayOfWeek: 2,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '08-10',
    //     CourseId: 'POO002',
    //     CourseName: 'Programare orientata-obiect',
    //     CourseType: 'Curs',
    //     ProfessorId: 'GD001,LD001',
    //     ProfessorName: 'Conf. dr. Gavrilut Dragos, Prof. dr. Lucanu Dorel',
    //     Classroom: 'C2',
    //     DayOfWeek: 3,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '10-12',
    //     CourseId: 'ENG001',
    //     CourseName: 'Limba engleza 2',
    //     CourseType: 'Seminar',
    //     ProfessorId: 'AN001',
    //     ProfessorName: 'Lect. dr. Armanu Nicoleta',
    //     Classroom: 'C903',
    //     DayOfWeek: 3,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '14-16',
    //     CourseId: 'PA002',
    //     CourseName: 'Proiectarea algoritmilor',
    //     CourseType: 'Seminar',
    //     ProfessorId: 'CS001',
    //     ProfessorName: 'Conf. dr. Ciobâcă Ștefan',
    //     Classroom: 'C905',
    //     DayOfWeek: 3,
    //     StudentGroup: '3B4'
    //   },
    //   {
    //     TimeInterval: '08-10',
    //     CourseId: 'SO002',
    //     CourseName: 'Sisteme de Operare',
    //     CourseType: 'Laborator',
    //     ProfessorId: 'SA001',
    //     ProfessorName: 'Asist. dr. Scutelnicu Andrei',
    //     Classroom: 'C401',
    //     DayOfWeek: 4,
    //     StudentGroup: '3B4'
    //   }
    // ];
  }

  getTimetableEntries() {
    let user = this.storageService.getUser();

    if (user.role !== 'Professor') {
      console.log('User not a professor!');
      return;
    }

    this.timetableService
      .getTimetableEntriesByStudentGroupName('1A1')
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
      (entry) => entry.DayOfWeek === dayIndex + 1
    );
  }
}
