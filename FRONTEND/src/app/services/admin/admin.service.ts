import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from 'src/app/interfaces/account/user';
import { UsersCount } from 'src/app/interfaces/admin/usersCount';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseURL = environment.baseURL;

  httpOptions: { headers: HttpHeaders } = {
    headers: new HttpHeaders({ "Content-Type": "application/json" }),
  };

  constructor(private http: HttpClient) { }

  getUsersCount(): Observable<UsersCount> {
    return this.http.get<UsersCount>(this.baseURL + 'admin/usersCount', this.httpOptions);
  };

  getUsersWithRoles(): Observable<User[]> {
    return this.http.get<User[]>(`${this.baseURL}admin/users-with-roles`, this.httpOptions);
  };

  updateUserRoles(username: string, roles: string[]): Observable<string[]> {
    const rolesAsString = roles.join(',');

    return this.http.post<string[]>(`${this.baseURL}admin/edit-roles/${username}?roles=${rolesAsString}`, this.httpOptions);
  };

  updateUserInfo(username: string, model: {}): Observable<any> {
    return this.http.put(this.baseURL + 'admin/edit-member-info/' + username, model, this.httpOptions);
  };

  lockUser(usernId: number, userName: {}): Observable<any> {
    return this.http.put(this.baseURL + 'admin/lock-member/' + usernId, userName, this.httpOptions);
  };

  unlockUser(usernId: number, userName: {}): Observable<any> {
    return this.http.put(this.baseURL + 'admin/unlock-member/' + usernId, userName, this.httpOptions);
  };

  deleteUser(userId: number, username: {}): Observable<any> {
    const url = `${this.baseURL}admin/delete-member/${userId}`;
    const options = { body: username };
    return this.http.delete(url, options);
  };
};
