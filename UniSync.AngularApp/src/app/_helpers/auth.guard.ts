import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs';
import {StorageService} from "../_services/storage.service";

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(private storageService: StorageService, private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | Promise<boolean> | boolean {
    const isLoggedIn = this.storageService.isLoggedIn();
    if (!isLoggedIn) {
      // Redirect to the login page
      this.router.navigate(['/login']);
      console.log("REDIRECTED")
      return false;
    }
    console.log("IS ALREADY LOGGED IN!");
    return true;
  }
}


