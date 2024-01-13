import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalculatorColService {
  baseURL = environment.baseURL;

  httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
  };

  constructor(private http: HttpClient) { }

  calculateCOL(model: any): Observable<any> {
    return this.http.post(this.baseURL + 'cholesterol/calculateCol', model, this.httpOptions);
  };

  getColResults(): Observable<any> {
    return this.http.get(this.baseURL + 'cholesterol/cholesterolCategoryStats', this.httpOptions);
  };
}
