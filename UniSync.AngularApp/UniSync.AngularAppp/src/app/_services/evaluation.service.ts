import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { Evaluation } from '../_interfaces/evaluation';

const API_URL = environment.apiUrl;

@Injectable({
  providedIn: 'root'
})
export class EvaluationService {
  constructor(private http: HttpClient) {}

  addEvaluation(evaluationDto: Evaluation): Observable<any> {
    return this.http.post(`${API_URL}/Evaluations`, evaluationDto);
  }

  getEvaluationsByStudentId(studentId: string): Observable<any> {
    return this.http.get(`${API_URL}/Evaluations/Student/${studentId}`);
  }
}
