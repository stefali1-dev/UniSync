import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from "../../environments/environment";

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  getSearchedUsers(request: string): Observable<any> {
    return this.http.get(API_URL + `/Users/search/${request}`);

  }
}