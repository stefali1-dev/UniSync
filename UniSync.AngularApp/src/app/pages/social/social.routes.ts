import { VexRoutes } from '@vex/interfaces/vex-route.interface';

const routes: VexRoutes = [
  {
    path: '',
    loadComponent: () =>
      import('./own-social-profile/own-social-profile.component').then(
        (m) => m.OwnSocialProfileComponent
      ),
    data: {
      toolbarShadowEnabled: true
    }
  },
  {
    path: 'edit',
    loadComponent: () =>
      import('./edit-profile/edit-profile.component').then(
        (m) => m.EditProfileComponent
      )
  },
  {
    path: ':userId',
    loadComponent: () =>
      import('./user-social-profile/user-social-profile.component').then(
        (m) => m.UserSocialProfileComponent
      )
  }
];

export default routes;
