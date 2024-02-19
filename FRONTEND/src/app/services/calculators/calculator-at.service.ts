import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CalculatorAtService {
  baseURL = environment.baseURL;

  httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
  };

  constructor(private http: HttpClient) { }

  calculateAT(model: any): Observable<any> {
    return this.http.post(this.baseURL + 'arterialtension/calculateAT', model, this.httpOptions);
  };

  getATResults(): Observable<any> {
    return this.http.get(this.baseURL + 'arterialtension/arterialTensionCategoryStats', this.httpOptions);
  };

  getATResultsForUser(): Observable<any> {
    return this.http.get(this.baseURL + 'arterialtension/getUserArterialTensionResults', this.httpOptions);
  };

  deleteUserATs(): Observable<any> {
    const url = `${this.baseURL}arterialtension/deleteATs`;
    return this.http.delete(url, { responseType: 'text' });
  };
}
