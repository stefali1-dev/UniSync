import { Component, OnInit, DestroyRef, inject } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { VexPageLayoutContentDirective } from '@vex/components/vex-page-layout/vex-page-layout-content.directive';
import { VexBreadcrumbsComponent } from '@vex/components/vex-breadcrumbs/vex-breadcrumbs.component';
import { VexPageLayoutHeaderDirective } from '@vex/components/vex-page-layout/vex-page-layout-header.directive';
import { VexPageLayoutComponent } from '@vex/components/vex-page-layout/vex-page-layout.component';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MatIconModule } from '@angular/material/icon';
import { NgIf } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'vex-page-layout-simple-large-header-tabbed',
  templateUrl: './enrolled-course-page.component.html',
  standalone: true,
  imports: [
    VexPageLayoutComponent,
    VexPageLayoutHeaderDirective,
    VexBreadcrumbsComponent,
    VexPageLayoutContentDirective,
    MatTabsModule,
    MatIconModule,
    NgIf,
    FormsModule
  ]
})
export class EnrolledCoursePageComponent implements OnInit {
  courseId!: string;

  private readonly destroyRef: DestroyRef = inject(DestroyRef);

  isEditMode = false;
  courseData = {
    description: `The <strong>Data Structures</strong> course explores fundamental concepts related to data organization and manipulation. It covers various data structures commonly used in solving computational problems, including:
                  <ul class="list-disc ml-8 mt-4">
                    <li class="text-lg leading-relaxed">Arrays: Fundamental data structure for storing elements.</li>
                    <li class="text-lg leading-relaxed">Linked Lists: Dynamic structures for efficient insertions and deletions.</li>
                    <li class="text-lg leading-relaxed">Stacks and Queues: Abstract data types for managing data in specific orders.</li>
                    <li class="text-lg leading-relaxed">Trees: Understanding tree structures and traversal techniques.</li>
                  </ul>
                  Students will learn how to implement these structures in different programming languages and explore their practical applications.`,
    evaluation: `The course evaluation consists of two components:
                  <ul class="list-disc ml-8 mt-4">
                    <li class="text-lg leading-relaxed">
                      <strong>Ongoing Laboratory Evaluation:</strong> Throughout the semester, there will be three laboratory exams assessing practical skills and understanding of course material.
                    </li>
                    <li class="text-lg leading-relaxed">
                      <strong>Final Exam:</strong> At the end of the semester, a comprehensive final exam will take place. To pass, students need a minimum score of 4.5 in the final exam and a median score of at least 4.5 across the three laboratory exams.
                    </li>
                  </ul>`,
    laboratory: `In the laboratory sessions, students will delve deeper into the algorithms presented during the course. Expect practical exercises, problem-solving, and collaborative learning. These sessions will enhance your understanding of data structures and their real-world applications.`
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    //this.router.routeReuseStrategy.shouldReuseRoute = () => false;
  }

  ngOnInit() {
    this.route.paramMap
      .pipe(
        map((paramMap) => paramMap.get('courseId')),
        takeUntilDestroyed(this.destroyRef)
      )
      .subscribe((courseId) => {
        //this.messages = [];

        if (!courseId) {
          throw new Error('Chat id not found!');
        }

        this.courseId = courseId;
      });
  }

  toggleEdit() {
    this.isEditMode = !this.isEditMode;
  }

  saveChanges() {
    // Send the modified data to the server
    //this.courseData
    console.log('sal');

    this.toggleEdit();

    this.router.navigate(['/apps/courses/teaching/' + this.courseId]);
  }
}
