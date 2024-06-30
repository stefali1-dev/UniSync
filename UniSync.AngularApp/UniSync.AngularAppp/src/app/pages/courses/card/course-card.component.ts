// course-card.component.ts
import { Component, Input, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
@Component({
  selector: 'app-course-card',
  standalone: true,
  templateUrl: './course-card.component.html',  imports: [
    RouterLink
  ]
})
export class CourseCardComponent implements OnInit {
    @Input() course: {
      title: string;
      imageUrl: string;
      description: string;
      courseId: string;
    } = {
      title: '',
      imageUrl: '',
      description: '',
      courseId: '1'
    };

    constructor(private router: Router) {}
    
    ngOnInit() {}

  }