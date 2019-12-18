import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { ApiRoutes } from 'src/core/utilities/api-routes';
import { StudentResult } from './studentResult.model';
import { StudentResultRequst } from './StudentResultRequest.model';

@Injectable({
  providedIn: 'root'
})
export class DiaryService {
  constructor(private http: HttpClient) { }

  public getResults(): Observable<StudentResult[]> {
    return this.http.get<StudentResult[]>(ApiRoutes.studentResults);
  }

  public createResult(subjectId: number, studentId: number, studentRequest: StudentResultRequst): Observable<StudentResult> {
    return this.http.put<StudentResult>(`${ApiRoutes.studentResults}/subjects/${subjectId}/students/${studentId}`, studentRequest);
  }

  public updateResult(resultId: number, studentRequest: StudentResultRequst): Observable<StudentResult> {
    return this.http.put<StudentResult>(`${ApiRoutes.studentResults}/${resultId}`, studentRequest);
  }
}
