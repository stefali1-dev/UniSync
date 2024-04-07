import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from "../../environments/environment";

const API_URL = environment.apiUrl;

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
      API_URL + '/Authentication/login',
      {
        email,
        password,
      },
      {responseType: 'text'}
    );
  }

  register(registrationId: string, email: string, password: string): Observable<any> {
    return this.http.post(
      API_URL + '/Authentication/register',
      {
        registrationId,
        email,
        password,
      },
      {responseType: 'text'}
    );
  }

  logout(): Observable<any> {
    return this.http.post(API_URL + '/Authentication/logout', { }, httpOptions);
  }

  getCurrentUserInfo(): Observable<any>{
    return this.http.get(API_URL + '/Authentication/currentuserinfo');
  }
}