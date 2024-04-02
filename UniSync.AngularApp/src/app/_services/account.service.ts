import { Injectable } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {BehaviorSubject, map, Observable} from "rxjs";
import {User} from "../_models/user";
import {environment} from "../../environments/environment";
import { HttpErrorResponse } from '@angular/common/http';
import { tap, catchError } from 'rxjs/operators';
import { HttpResponse } from '@angular/common/http';




@Injectable({
  providedIn: 'root'
})
export class AccountService {
  baseUrl =  environment.apiUrl;
  private currentUserSource = new BehaviorSubject<User | null>(null);
  currentUser$ = this.currentUserSource.asObservable();
  private token: string | null = null;

  constructor(private http: HttpClient) {
    // Retrieve token from localStorage on service initialization
    this.token = localStorage.getItem('token');
  }

  register(userData: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/Authentication/register`, userData);
  }

  login(credentials: { email: string; password: string }): Observable<HttpResponse<string>> {
    return this.http.post<string>(`${this.baseUrl}/Authentication/login`, credentials, { observe: 'response' }).pipe(
      tap((response) => {
        if (response.status === 200) {
          // Store the token in localStorage and service property
          const token = response.body;
          localStorage.setItem('token', token || '');
          this.token = token || null;
        }
      }),
      catchError((error: HttpErrorResponse) => {
        if (error.status === 400) {
          // Handle BadRequest error
          console.error('Login error:', error.error);
          throw new Error(error.error || 'Bad request');
        } else if (error.status === 500) {
          // Handle InternalServerError
          console.error('Login error:', error.error);
          throw new Error('Internal server error');
        } else {
          // Handle other errors
          console.error('Login error:', error);
          throw error;
        }
      })
    );
  }

  logout(): void {
    // Remove the token from localStorage and service property
    localStorage.removeItem('token');
    this.token = null;
    // Optionally, you can also handle other logout-related tasks here
  }

  isLoggedIn(): boolean {
    return !!this.token; // Return true if token exists, false otherwise
  }

  getToken(): string | null {
    return this.token;
  }
}


