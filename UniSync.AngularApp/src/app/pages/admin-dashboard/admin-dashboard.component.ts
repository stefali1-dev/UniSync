import { Component } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { NgIf } from '@angular/common';

@Component({
  selector: 'app-admin-dashboard',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf],
  templateUrl: './admin-dashboard.component.html'
})
export class AdminDashboardComponent {
  selectedModal: string = '';
  addStudentForm: FormGroup;
  addProfessorForm: FormGroup;
  createCourseForm: FormGroup;

  // Declare other form groups for each modal

  constructor(private formBuilder: FormBuilder) {
    this.addStudentForm = this.formBuilder.group({
      registrationId: ['', Validators.required],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      year: ['', Validators.required],
      group: ['', Validators.required]
    });

    this.addProfessorForm = this.formBuilder.group({
      registrationId: ['', Validators.required],
      firstname: ['', Validators.required],
      lastname: ['', Validators.required],
      title: ['', Validators.required]
    });

    this.createCourseForm = this.formBuilder.group({
      courseId: ['', Validators.required],
      name: ['', Validators.required],
      credits: ['', [Validators.required, Validators.min(1)]],
      semester: ['', Validators.required]
    });

    // Initialize other form groups
  }

  openModal(modalName: string) {
    this.selectedModal = modalName;
  }

  closeModal() {
    this.selectedModal = '';
  }

  addStudent() {
    if (this.addStudentForm.valid) {
      // Perform the logic to add a student using the form data
      console.log(this.addStudentForm.value);
      this.addStudentForm.reset();
      this.closeModal();
    }
  }

  removeStudent() {
    // Logic to remove a student
  }

  enrollStudent() {
    // Logic to enroll a student
  }

  unenrollStudent() {
    // Logic to unenroll a student
  }

  addProfessor() {
    if (this.addProfessorForm.valid) {
      // Perform the logic to add a professor using the form data
      console.log('Adding professor:', this.addProfessorForm.value);
      this.addProfessorForm.reset();
      this.closeModal();
    }
  }

  removeProfessor() {
    // Logic to remove a professor
  }

  addToCourse() {
    // Logic to add a professor to a course
  }

  removeFromCourse() {
    // Logic to remove a professor from a course
  }

  createCourse() {
    if (this.createCourseForm.valid) {
      // Perform the logic to create a course using the form data
      console.log('Creating course:', this.createCourseForm.value);
      this.createCourseForm.reset();
      this.closeModal();
    }
  }

  deleteCourse() {
    // Logic to delete a course
  }
}
