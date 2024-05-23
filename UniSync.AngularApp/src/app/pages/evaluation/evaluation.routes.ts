import { VexRoutes } from '@vex/interfaces/vex-route.interface';

const routes: VexRoutes = [
  {
    path: '',
    loadComponent: () =>
      import('./evaluation-home.component').then(
        (m) => m.EvaluationHomeComponent
      ),
    data: {
      toolbarShadowEnabled: true
    }
  },
  {
    path: 'professor/:studentId',
    loadComponent: () =>
      import('./student-page/student-page.component').then(
        (m) => m.StudentPageComponent
      )
  },
  {
    path: 'student/:studentId',
    loadComponent: () =>
      import('./student-self-page/student-self-page.component').then(
        (m) => m.StudentSelfPageComponent
      )
  }
];

export default routes;
