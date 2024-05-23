import { Component } from '@angular/core';
import { CommonModule, NgClass, NgFor, NgIf } from '@angular/common';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatRadioModule } from '@angular/material/radio';
import { MatSliderModule } from '@angular/material/slider';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { AsyncPipe} from '@angular/common';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';


@Component({
  selector: 'app-student-page',
  standalone: true,
  templateUrl: './student-page.component.html',
  imports: [
    MatButtonModule,
    MatIconModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatOptionModule,
    NgIf,
    ReactiveFormsModule,
    MatAutocompleteModule,
    NgFor,
    MatDatepickerModule,
    MatSliderModule,
    MatRadioModule,
    MatSlideToggleModule,
    MatCheckboxModule,
    AsyncPipe,
    NgClass,
  ]
})
export class StudentPageComponent {
  studentName = 'John Doe';
  studentGroup = 'CS301';
  enrolledCourses = ['Introduction to Programming', 'Data Structures and Algorithms', 'Database Systems'];
  stateCtrl = new UntypedFormControl();

  states = [
    {
      name: 'Arkansas',
      population: '2.978M',
      // https://commons.wikimedia.org/wiki/File:Flag_of_Arkansas.svg
      flag: 'https://upload.wikimedia.org/wikipedia/commons/9/9d/Flag_of_Arkansas.svg'
    },
    {
      name: 'California',
      population: '39.14M',
      // https://commons.wikimedia.org/wiki/File:Flag_of_California.svg
      flag: 'https://upload.wikimedia.org/wikipedia/commons/0/01/Flag_of_California.svg'
    },
    {
      name: 'Florida',
      population: '20.27M',
      // https://commons.wikimedia.org/wiki/File:Flag_of_Florida.svg
      flag: 'https://upload.wikimedia.org/wikipedia/commons/f/f7/Flag_of_Florida.svg'
    },
    {
      name: 'Texas',
      population: '27.47M',
      // https://commons.wikimedia.org/wiki/File:Flag_of_Texas.svg
      flag: 'https://upload.wikimedia.org/wikipedia/commons/f/f7/Flag_of_Texas.svg'
    }
  ];
  filteredStates$ = this.stateCtrl.valueChanges.pipe(
    startWith(''),
    map((state) => (state ? this.filterStates(state) : this.states.slice()))
  );

  courseDetails = [
    {
      name: 'Introduction to Programming',
      labActivities: ['Hello World', 'Basic Data Types', 'Control Structures'],
      attendance: 90,
      examScore: 85,
    },
    {
      name: 'Data Structures and Algorithms',
      labActivities: ['Arrays', 'Linked Lists', 'Trees'],
      attendance: 92,
      examScore: 88,
    },
    // Add more course details as needed
  ];

  selectedCourse: any;
  showModal = false;

  openGradingModal(course: any) {
    this.selectedCourse = course;
    this.showModal = true;
  }

  closeModal() {
    this.selectedCourse = null;
    this.showModal = false;
  }

  saveGrading() {
    // Implement logic to save the grading data
    console.log('Grading saved for:', this.selectedCourse.name);
    this.closeModal()
  }

  filterStates(name: string) {
    return this.states.filter(
      (state) => state.name.toLowerCase().indexOf(name.toLowerCase()) === 0
    );
  }
}