import { VexRoutes } from '@vex/interfaces/vex-route.interface';

const routes: VexRoutes = [
  {
    path: 'enrolled',
    loadComponent: () =>
      import('./enrolled/course-list/enrolled-courses-list.component').then(
        (m) => m.CourseListComponent
      ),
    data: {
      toolbarShadowEnabled: true
    }
  },
  {
    path: 'enrolled/:courseId',
    loadComponent: () =>
      import('./enrolled/course-page/enrolled-course-page.component').then(
        (m) => m.EnrolledCoursePageComponent
      ),
    data: {
      toolbarShadowEnabled: true
    }
  },
  {
    path: 'teaching',
    loadComponent: () =>
      import('./teaching/course-list/teaching-courses-list.component').then(
        (m) => m.TeachingCourseListComponent
      ),
    data: {
      toolbarShadowEnabled: true
    }
  },
  {
    path: 'teaching/:courseId',
    loadComponent: () =>
      import('./teaching/course-page/teaching-course-page.component').then(
        (m) => m.TeachingCoursePageComponent
      ),
    data: {
      toolbarShadowEnabled: true
    }
  }
];

export default routes;
