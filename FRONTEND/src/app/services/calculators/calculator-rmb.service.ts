import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalculatorRmbService {
  baseURL = environment.baseURL;

  httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
  };

  constructor(private http: HttpClient) { }

  calculateRmb(model: any): Observable<any> {
    return this.http.post(this.baseURL + 'rmb/calculateRmb', model, this.httpOptions);
  };

  getUserResults(): Observable<any> {
    return this.http.get(this.baseURL + 'rmb/getUserRmbResults', this.httpOptions);
  };

  getRMBResultsForUser(): Observable<any> {
    return this.http.get(this.baseURL + 'rmb/getUserRmbResults', this.httpOptions);
  };
}
