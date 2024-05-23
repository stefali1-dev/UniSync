import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CourseCardComponent } from '../card/course-card.component';

@Component({
  selector: 'app-course-list',
  standalone: true,
  imports: [CommonModule, CourseCardComponent],
  templateUrl: './courses-list.component.html',
})
export class CourseListComponent {
  courses = [
    {
      title: 'Introduction to Web Development',
      imageUrl: 'https://example.com/course-image-1.jpg',
      description:
        'Learn the fundamentals of building web applications using HTML, CSS, and JavaScript.',
      courseId: '1'
    },
    {
      title: 'Advanced React',
      imageUrl: 'https://example.com/course-image-2.jpg',
      description:
        'Take your React skills to the next level by mastering advanced concepts and best practices.',
        courseId: '1'
    },
    {
      title: 'Node.js for Beginners',
      imageUrl: 'https://example.com/course-image-3.jpg',
      description:
        'Get started with Node.js and learn how to build server-side applications with JavaScript.',
        courseId: '1'
    },
    {
      title: 'Mastering Angular',
      imageUrl: 'https://example.com/course-image-4.jpg',
      description:
        'Dive deep into Angular and learn how to build powerful and scalable web applications.',
        courseId: '1'
    },
  ];
}