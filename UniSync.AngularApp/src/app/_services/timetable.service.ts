import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class TimetableService {
  constructor(private http: HttpClient) {}

  addTimetableEntry(timetableEntryDto: any): Observable<any> {
    return this.http.post(`${API_URL}/TimetableEntry`, timetableEntryDto);
  }
  getTimetableEntriesByProfessorId(professorId: string): Observable<any> {
    return this.http.get(
      `${API_URL}/TimetableEntry/ByProfessorId/${professorId}`
    );
  }

  getTimetableEntriesByStudentGroupName(groupName: string): Observable<any> {
    return this.http.get(
      `${API_URL}/TimetableEntry/ByStudentGroupName/${groupName}`
    );
  }
}
