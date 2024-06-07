import { Component, OnInit, DestroyRef, inject } from '@angular/core';
import { MatTabsModule } from '@angular/material/tabs';
import { VexPageLayoutContentDirective } from '@vex/components/vex-page-layout/vex-page-layout-content.directive';
import { VexBreadcrumbsComponent } from '@vex/components/vex-breadcrumbs/vex-breadcrumbs.component';
import { VexPageLayoutHeaderDirective } from '@vex/components/vex-page-layout/vex-page-layout-header.directive';
import { VexPageLayoutComponent } from '@vex/components/vex-page-layout/vex-page-layout.component';
import { ActivatedRoute } from '@angular/router';
import { map } from 'rxjs/operators';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'vex-page-layout-simple-large-header-tabbed',
  templateUrl: './teaching-course-page.component.html',
  standalone: true,
  imports: [
    VexPageLayoutComponent,
    VexPageLayoutHeaderDirective,
    VexBreadcrumbsComponent,
    VexPageLayoutContentDirective,
    MatTabsModule
  ]
})
export class TeachingCoursePageComponent implements OnInit {
  constructor(private route: ActivatedRoute) {}
  private readonly destroyRef: DestroyRef = inject(DestroyRef);

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
      });
  }
}
