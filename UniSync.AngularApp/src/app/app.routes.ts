import { LayoutComponent } from './layouts/layout/layout.component';
import { VexRoutes } from '@vex/interfaces/vex-route.interface';
import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from '@angular/router';
import { AuthGuard } from './_helpers/auth.guard';
import { RoleGuard } from './_helpers/role.guard';

export const appRoutes: VexRoutes = [
  {
    path: 'login',
    loadComponent: () =>
      import('./pages/auth/login/login.component').then((m) => m.LoginComponent)
  },
  {
    path: 'register',
    loadComponent: () =>
      import('./pages/auth/register/register.component').then(
        (m) => m.RegisterComponent
      )
  },
  {
    path: 'forgot-password',
    loadComponent: () =>
      import('./pages/auth/forgot-password/forgot-password.component').then(
        (m) => m.ForgotPasswordComponent
      )
  },
  {
    path: 'coming-soon',
    loadComponent: () =>
      import('./pages/coming-soon/coming-soon.component').then(
        (m) => m.ComingSoonComponent
      )
  },
  {
    path: '',
    component: LayoutComponent,
    canActivate: [AuthGuard],
    children: [
      {
        path: 'apps',
        children: [
          {
            path: 'chat',
            loadChildren: () => import('./pages/chat/chat.routes')
          },
          {
            path: 'contacts',
            loadChildren: () => import('./pages/contacts/contacts.routes')
          },
          {
            path: 'profile',
            loadChildren: () => import('./pages/social/social.routes')
          },
          {
            path: 'courses',
            canActivate: [RoleGuard],
            loadChildren: () => import('./pages/courses/courses.routes')
          },
          {
            path: 'evaluation',
            canActivate: [RoleGuard],
            loadChildren: () => import('./pages/evaluation/evaluation.routes')
          },
          {
            path: 'timetable',
            loadComponent: () =>
              import(
                './pages/timetable/timetable-professor/timetable-professor.component'
              ).then((m) => m.TimetableComponent)
          }
        ]
      },
      {
        path: 'home',
        canActivate: [RoleGuard],
        loadChildren: () => import('./pages/home/home.routes')
      }
      // {
      //   path: 'admin-dashboard',
      //   canActivate: [RoleGuard],
      //   loadChildren: () =>
      //     import('./pages/admin-dashboard/dashboard.routes')
      // }
    ]
  }
];
