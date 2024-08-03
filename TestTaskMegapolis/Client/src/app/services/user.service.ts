import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../enviroments/environment';
import { UserGroup } from '../models/user-group';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = environment.apiUrl;
  private usersSubject = new BehaviorSubject<UserGroup[]>([]);
  users$ = this.usersSubject.asObservable();

  constructor(private http: HttpClient) { }

  getUsersWithGroups(): Observable<UserGroup[]> {
    return this.http.get<UserGroup[]>(this.apiUrl + 'User/get-users-with-groups');
  }

  updateUsers(): void {
    this.getUsersWithGroups().subscribe(users => this.usersSubject.next(users));
  }

  createUser(user: any): Observable<any> {
    return this.http.post(this.apiUrl + 'User/create', user);
  }
}

