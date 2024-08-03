import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Group } from '../models/group';
import { environment } from '../enviroments/environment';


@Injectable({
  providedIn: 'root'
})
export class GroupService {
  private apiUrl = `${environment.apiUrl}Group`;
  private groupsSubject = new BehaviorSubject<Group[]>([]);
  groups$ = this.groupsSubject.asObservable();

  constructor(private http: HttpClient) { 
    this.loadGroups();
  }

  private loadGroups(): void {
    this.http.get<Group[]>(this.apiUrl).subscribe(groups => this.groupsSubject.next(groups));
  }

  getGroups(): Observable<Group[]> {
    return this.groups$;
  }

  createGroup(groupName: string): Observable<any> {
    return this.http.post(`${environment.apiUrl}Group/create`, { name: groupName });
  }

  updateGroups(): void {
    this.loadGroups(); 
  }
}
