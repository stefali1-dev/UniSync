import { Component } from '@angular/core';
import { CommonModule, NgClass, NgFor, NgIf } from '@angular/common';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatRadioModule } from '@angular/material/radio';
import { MatSliderModule } from '@angular/material/slider';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { AsyncPipe } from '@angular/common';
import { MatOptionModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { ReactiveFormsModule, UntypedFormControl } from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { StorageService } from 'src/app/_services/storage.service';
import { StudentService } from 'src/app/_services/student.service';
import { UserService } from 'src/app/_services/user.service';
import { OnInit } from '@angular/core';

@Component({
  selector: 'app-student-page',
  standalone: true,
  templateUrl: './student-self-page.component.html',
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
    NgClass
  ]
})
export class StudentSelfPageComponent implements OnInit {
  studentName = 'John Doe';
  studentGroup = 'CS301';
  enrolledCourses = [
    'Introduction to Programming',
    'Data Structures and Algorithms',
    'Database Systems'
  ];
  stateCtrl = new UntypedFormControl();

  courseDetails = [
    {
      name: 'Introduction to Programming',
      labActivities: ['Hello World', 'Basic Data Types', 'Control Structures'],
      attendance: 90,
      examScore: 85
    },
    {
      name: 'Data Structures and Algorithms',
      labActivities: ['Arrays', 'Linked Lists', 'Trees'],
      attendance: 92,
      examScore: 88
    }
    // Add more course details as needed
  ];

  selectedCourse: any;
  showModal = false;

  constructor(
    private storageService: StorageService,
    private studentService: StudentService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.getStudentInfo();
  }

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
    this.closeModal();
  }

  getStudentInfo() {
    let userId = this.storageService.getUser().userId();

    this.userService.getUserById(userId).subscribe({
      next: (data) => {
        console.log(data);
      },
      error: (err) => {
        console.log(err);
      }
    });
  }
}
