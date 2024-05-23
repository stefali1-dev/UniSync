import { VexRoutes } from '@vex/interfaces/vex-route.interface';

const routes: VexRoutes = [
  {
    path: '',
    loadComponent: () =>
      import('./list/courses-list.component').then(
        (m) => m.CourseListComponent
      ),
    data: {
      toolbarShadowEnabled: true
    }
  },
  {
    path: ':courseId',
    loadComponent: () =>
      import('./course-page.component').then(
        (m) => m.CoursePageComponent
      ),
    data: {
      toolbarShadowEnabled: true
    }
  }
];

export default routes;
