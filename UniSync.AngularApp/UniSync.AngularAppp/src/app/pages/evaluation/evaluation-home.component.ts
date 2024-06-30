import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import {StudentListComponent} from './student-list/student-list.component';

@Component({
  selector: 'evaluation-home',
  standalone: true,
  imports: [CommonModule,StudentListComponent],
  templateUrl: './evaluation-home.component.html',
})
export class EvaluationHomeComponent {
}