import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class StudentService {
  constructor(private http: HttpClient) {}

  getStudentsByGroup(groupName: string): Observable<any> {
    return this.http.get(API_URL + `/Students/ByGroup/${groupName}`);
  }

  getAllStudents(): Observable<any> {
    return this.http.get(API_URL + '/Users/Students');
  }

  searchStudents(searchValue: string): Observable<any> {
    return this.http.get(
      API_URL + `/Users/Students/search?searchValue=${searchValue}`
    );
  }

  getStudentById(appUserId: string): Observable<any> {
    return this.http.get(API_URL + `/Students/ById/${appUserId}`);
  }
  getStudentByChatUserId(chatUserId: string): Observable<any> {
    return this.http.get(API_URL + `/Students/ByChatUserId/${chatUserId}`);
  }
}
