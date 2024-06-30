import { Injectable } from '@angular/core';
import {
  CanActivate,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  Router
} from '@angular/router';
import { StorageService } from '../_services/storage.service';

@Injectable({ providedIn: 'root' })
export class RoleGuard implements CanActivate {
  constructor(
    private router: Router,
    private storageService: StorageService
  ) {}

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const userRole = this.storageService.getUser().role;
    const userId = this.storageService.getUser().userId;

    if (userRole === 'Student') {
      if (state.url === '/home') {
        this.router.navigate(['/home/student']);
      } else if (state.url === '/apps/courses') {
        this.router.navigate(['apps/courses/enrolled']);
      } else if (state.url === '/apps/evaluation') {
        this.router.navigate([`/apps/evaluation/self/${userId}`]);
      }
    } else if (userRole === 'Professor') {
      if (state.url === '/home') {
        this.router.navigate(['/home/professor']);
      } else if (state.url === '/apps/courses') {
        this.router.navigate(['apps/courses/teaching']);
      }
    } else if (userRole === 'Admin') {
      if (state.url === '/home') {
        this.router.navigate(['/home/admin']);
      }
    }

    return true;
  }
}
