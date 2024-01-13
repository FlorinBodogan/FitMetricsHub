import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalculatorBmiService {
  baseURL = environment.baseURL;

  httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
  };

  constructor(private http: HttpClient) { }

  calculateBmi(model: any): Observable<any> {
    return this.http.post(this.baseURL + 'bmi/calculateBmi', model, this.httpOptions);
  };

  getUserResults(): Observable<any> {
    return this.http.get(this.baseURL + 'bmi/getUserBmiResults', this.httpOptions);
  };
}
