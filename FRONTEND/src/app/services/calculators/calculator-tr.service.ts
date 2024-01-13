import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalculatorTrService {
  baseURL = environment.baseURL;

  httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
  };

  constructor(private http: HttpClient) { }

  calculateTri(model: any): Observable<any> {
    return this.http.post(this.baseURL + 'triglycerides/calculateTri', model, this.httpOptions);
  };

  getTriResults(): Observable<any> {
    return this.http.get(this.baseURL + 'triglycerides/triglyceridesCategoryStats', this.httpOptions);
  };
}
