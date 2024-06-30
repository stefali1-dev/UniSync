import { VexRoutes } from '@vex/interfaces/vex-route.interface';

const routes: VexRoutes = [
  {
    path: '',
    loadComponent: () =>
      import('./users-list/users-list.component').then(
        (m) => m.UsersListComponent
      ),
    data: {
      scrollDisabled: true,
      toolbarShadowEnabled: true
    }
  }
];

export default routes;
