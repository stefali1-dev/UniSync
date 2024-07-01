// @ts-nocheck

import { Component, OnInit } from '@angular/core';
import { scaleIn400ms } from '@vex/animations/scale-in.animation';
import { fadeInRight400ms } from '@vex/animations/fade-in-right.animation';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  UntypedFormControl,
  Validators
} from '@angular/forms';
import { debounceTime } from 'rxjs/operators';
import { stagger40ms } from '@vex/animations/stagger.animation';
import { MatDialog } from '@angular/material/dialog';
import { AsyncPipe, NgFor, NgIf } from '@angular/common';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { Router } from '@angular/router';
import { ChangeDetectorRef } from '@angular/core';

import { TimetableService } from 'src/app/_services/timetable.service';

export interface TimetableEntry {
  classroom: string;
  courseId: string;
  courseName: string;
  courseType: string;
  dayOfWeek: number; // 0 represents Sunday, 1 represents Monday, and so on
  professorId: string;
  professorName: string;
  studentGroup: string;
  timeInterval: string;
  timetableEntryId: string;
}

@Component({
  selector: 'timetable-entry-list',
  templateUrl: './timetable-entry-list.component.html',
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
export class TimetableEntryListComponent implements OnInit {
  activeEntry: TimetableEntry | null = null;
  isDropdownOpen = false;

  selectedModal: string = '';
  addEntryForm: FormGroup;

  timetableEntries: TimetableEntry[] = [];
  searchCtrl = new UntypedFormControl();

  searchStr$ = this.searchCtrl.valueChanges.pipe(debounceTime(10));

  menuOpen = false;

  constructor(
    private dialog: MatDialog,
    private router: Router,
    private timetableService: TimetableService,
    private changeDetectorRef: ChangeDetectorRef,
    private formBuilder: FormBuilder
  ) {
    this.addEntryForm = this.formBuilder.group({
      timeInterval: ['', Validators.required],
      courseId: ['', Validators.required],
      courseName: ['', Validators.required],
      courseType: ['', Validators.required],
      professorId: ['', Validators.required],
      professorName: ['', Validators.required],
      classroom: ['', Validators.required],
      dayOfWeek: [
        0,
        [Validators.required, Validators.min(0), Validators.max(6)]
      ],
      studentGroup: ['', Validators.required]
    });
  }

  ngOnInit() {
    this.getAllTimetableEntries();
  }

  openMenu() {
    this.menuOpen = true;
  }

  openModal(modalName: string) {
    this.selectedModal = modalName;
  }

  closeModal() {
    this.selectedModal = '';
  }

  addTimetableEntry() {
    if (this.addEntryForm.valid) {
      // Perform the logic to add a timetable entry using the form data
      console.log(this.addEntryForm.value);
      this.addEntryForm.reset();

      let timetableEntry = {
        timeInterval: this.addEntryForm.value.timeInterval,
        courseId: this.addEntryForm.value.courseId,
        courseName: this.addEntryForm.value.courseName,
        courseType: this.addEntryForm.value.courseType,
        professorId: this.addEntryForm.value.professorId,
        professorName: this.addEntryForm.value.professorName,
        classroom: this.addEntryForm.value.classroom,
        dayOfWeek: this.addEntryForm.value.dayOfWeek, // 0 represents Sunday, 1 represents Monday, and so on
        studentGroup: this.addEntryForm.value.studentGroup
      };

      this.timetableService.addTimetableEntry(timetableEntry).subscribe({
        next: (res) => {
          console.log(res);
        }
      });

      this.closeModal();
    }
  }

  onEnterPressed(request: string) {
    // this.timetableService.searchTimetableEntries(request).subscribe({
    //   next: (entries) => {
    //     console.log(entries);
    //     this.timetableEntries = entries;
    //   },
    //   error: (err) => {
    //     console.log(err);
    //   }
    // });
  }

  getAllTimetableEntries() {
    this.timetableService.getAllTimetableEntries().subscribe({
      next: (data) => {
        this.timetableEntries = data.value;

        console.log(this.timetableEntries);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }

  toggleDropdown(entry: TimetableEntry): void {
    if (this.activeEntry === entry && this.isDropdownOpen) {
      this.isDropdownOpen = false;
    } else {
      this.activeEntry = entry;
      this.isDropdownOpen = true;
    }
  }

  removeEntry(entry: TimetableEntry): void {
    // Implement your remove entry logic here
    console.log(`Removing entry: ${entry.subject}`);
    // Example of removing entry from the list
    this.timetableEntries = this.timetableEntries.filter((e) => e !== entry);
    this.isDropdownOpen = false; // Close dropdown after action
  }
}
