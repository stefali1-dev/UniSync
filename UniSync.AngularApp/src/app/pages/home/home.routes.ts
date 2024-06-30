import { VexRoutes } from '@vex/interfaces/vex-route.interface';

const routes: VexRoutes = [
  {
    path: 'student',
    loadComponent: () =>
      import('./student/student-home.component').then(
        (m) => m.StudentHomeComponent
      ),
    data: {
      toolbarShadowEnabled: true
    }
  },
  {
    path: 'professor',
    loadComponent: () =>
      import('./professor/professor-home.component').then(
        (m) => m.ProfessorHomeComponent
      )
  },
  {
    path: 'admin',
    loadComponent: () =>
      import('./admin/admin-home.component').then((m) => m.AdminHomeComponent)
  },
  {
    path: 'admin/students',
    loadComponent: () =>
      import('./admin/student-list/student-list.component').then(
        (m) => m.StudentListComponent
      )
  },
  {
    path: 'admin/professors',
    loadComponent: () =>
      import('./admin/professor-list/professor-list.component').then(
        (m) => m.ProfessorListComponent
      )
  },
  {
    path: 'admin/courses',
    loadComponent: () =>
      import('./admin/course-list/course-list.component').then(
        (m) => m.CourseListComponent
      )
  },
  {
    path: 'admin/timetable',
    loadComponent: () =>
      import(
        './admin/timetable-entry-list/timetable-entry-list.component'
      ).then((m) => m.TimetableEntryListComponent)
  }
];

export default routes;
