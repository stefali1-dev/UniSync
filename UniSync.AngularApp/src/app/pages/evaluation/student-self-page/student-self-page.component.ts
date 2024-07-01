import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe, NgClass, NgFor, NgIf } from '@angular/common';
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
import {
  ReactiveFormsModule,
  UntypedFormControl,
  FormBuilder,
  FormGroup,
  Validators
} from '@angular/forms';
import { map, startWith } from 'rxjs/operators';
import { StorageService } from 'src/app/_services/storage.service';
import { StudentService } from 'src/app/_services/student.service';
import { UserService } from 'src/app/_services/user.service';
import { CourseService } from 'src/app/_services/course.service';
import { ActivatedRoute } from '@angular/router';
import { Course } from '../../courses/enrolled/course-list/enrolled-courses-list.component';
import { EvaluationService } from 'src/app/_services/evaluation.service';
import { Evaluation } from 'src/app/_interfaces/evaluation';
import { add } from 'date-fns';

export interface EvaluationView {
  courseId: string;
  type: string;
  courseName: string;
  grade: number;
  dateTime: Date;
  comment?: string;
}

export interface CourseView {
  courseId: string;
  courseName: string;
  courseNumber: string;
  credits: string;
  description: string;
  semester: string;
  evaluations: EvaluationView[];
}

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
    NgClass,
    DatePipe
  ]
})
export class StudentSelfPageComponent implements OnInit {
  studentId: any;
  studentAppUserId: string = '';
  studentName = 'John Doe';
  studentGroup = 'CS301';
  enrolledCourses: CourseView[] = [];
  stateCtrl = new UntypedFormControl();

  selectedCourse: any;
  showModal = false;

  gradingForm: FormGroup;

  constructor(
    private storageService: StorageService,
    private studentService: StudentService,
    private userService: UserService,
    private route: ActivatedRoute,
    private courseService: CourseService,
    private evaluationService: EvaluationService,
    private fb: FormBuilder
  ) {
    this.gradingForm = this.fb.group({
      gradingType: ['', Validators.required],
      grade: ['', Validators.required],
      gradingDate: ['', Validators.required],
      comment: ['']
    });
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('studentId');
    if (id !== null) {
      this.studentId = id;
      this.getStudentInfo();
    }
  }

  openGradingModal(course: any) {
    this.selectedCourse = course;
    this.showModal = true;
    // Reset the form when opening the modal
    this.gradingForm.reset();
  }

  closeModal() {
    this.selectedCourse = null;
    this.showModal = false;
  }

  saveGrading(courseId: string) {
    if (this.gradingForm.valid) {
      const formValues = this.gradingForm.value;

      let addedEvaluation: Evaluation = {
        studentId: this.studentId,
        courseId: courseId,
        professorId: this.storageService.getUser().userId,
        grade: Number(formValues.grade),
        dateTime: new Date(formValues.gradingDate),
        comment: formValues.comment
      };

      console.log(addedEvaluation);

      this.evaluationService.addEvaluation(addedEvaluation).subscribe({
        next: (res) => {
          //console.log(res);
        },
        error: (err) => {
          console.log(err);
        }
      });

      this.closeModal();
    }
  }

  getStudentInfo() {
    this.userService.getUserById(this.studentId).subscribe({
      next: (data) => {
        let user = data.user;

        this.studentName = user.firstName + ' ' + user.lastName;
      },
      error: (err) => {
        console.log(err);
      }
    });

    this.studentService.getStudentByChatUserId(this.studentId).subscribe({
      next: (student) => {
        this.studentGroup = student.group;
        let retreivedCourses: CourseView[] = [];
        this.studentAppUserId = student.studentId;
        let studentEvaluations: any;

        console.log(this.studentAppUserId);

        this.evaluationService
          .getEvaluationsByStudentId(this.studentAppUserId)
          .subscribe({
            next: (evaluations) => {
              console.log(evaluations);
              studentEvaluations = evaluations;

              student.coursesIds.forEach((courseId) => {
                this.courseService.getCoursesByCourseId(courseId).subscribe({
                  next: (c) => {
                    let evaluationViews: EvaluationView[] = [];

                    const courseEvaluations = studentEvaluations.filter(
                      (e) => e.courseId === c.courseId
                    );

                    courseEvaluations.forEach((e) => {
                      let ev: EvaluationView = {
                        courseId: e.courseId,
                        courseName: e.courseName,
                        grade: e.grade,
                        type: 'Lab',
                        dateTime: e.dateTime,
                        comment: e.comment
                      };

                      evaluationViews.push(ev);
                    });

                    let course: CourseView = {
                      courseId: c.courseId,
                      courseName: c.courseName,
                      courseNumber: c.courseNumber,
                      credits: c.credits,
                      description: c.description,
                      semester: c.semester,
                      evaluations: evaluationViews
                    };

                    retreivedCourses.push(course);
                  },
                  error: (err) => {
                    console.log(err);
                  }
                });
              });
            },
            error: (err) => {
              console.log(err);
            }
          });

        console.log(retreivedCourses);
        this.enrolledCourses = retreivedCourses;
      },

      error: (err) => {
        console.log(err);
      }
    });
  }
}
