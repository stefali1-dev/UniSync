import { SocialComponent } from './social.component';
import { VexRoutes } from '@vex/interfaces/vex-route.interface';

const routes: VexRoutes = [
  {
    path: '',
    component: SocialComponent,
    data: {
      toolbarShadowEnabled: true
    },
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./own-social-profile/own-social-profile.component').then(
            (m) => m.SocialProfileComponent
          )
      },
      {
        path: ':userId',
        loadComponent: () =>
          import('./user-social-profile/user-social-profile.component').then(
            (m) => m.UserSocialProfileComponent
          )
      }
    ]
  }
];

export default routes;
