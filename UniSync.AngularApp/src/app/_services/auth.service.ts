import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from "../../environments/environment";

const AUTH_API = environment.apiUrl;

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    return this.http.post(
      AUTH_API + '/Authentication/login',
      {
        email,
        password,
      },
      {responseType: 'text'}
    );
  }

  register(registrationId: string, email: string, password: string): Observable<any> {
    return this.http.post(
      AUTH_API + '/Authentication/register',
      {
        registrationId,
        email,
        password,
      },
      {responseType: 'text'}
    );
  }

  logout(): Observable<any> {
    return this.http.post(AUTH_API + '/Authentication/logout', { }, httpOptions);
  }

  getCurrentUserInfo(): Observable<any>{
    return this.http.get(AUTH_API + '/Authentication/currentuserinfo');
  }
}