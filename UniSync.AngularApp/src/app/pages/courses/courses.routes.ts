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
  }
];

export default routes;
