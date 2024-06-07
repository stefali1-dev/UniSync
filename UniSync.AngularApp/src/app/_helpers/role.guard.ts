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
    const userRole = this.storageService.getUser().role; // Update with your method

    if (userRole === 'Student' && state.url !== '/apps/courses/enrolled') {
      this.router.navigate(['apps/courses/enrolled']);
      return false;
    }

    if (userRole === 'Professor' && state.url !== '/apps/courses/teaching') {
      this.router.navigate(['apps/courses/teaching']);
      return false;
    }

    return true;
  }
}
