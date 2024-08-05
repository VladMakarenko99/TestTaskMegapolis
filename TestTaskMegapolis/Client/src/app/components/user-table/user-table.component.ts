import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

import { UserGroup } from '../../models/user-group';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-user-table',
  templateUrl: './user-table.component.html',
  styleUrls: ['./user-table.component.css'],
  standalone: true,
  imports: [CommonModule, HttpClientModule]
})
export class UserTableComponent implements OnInit {
  userGroups: UserGroup[] = [];
  errorMessage: string | null = null;

  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.users$.subscribe(users => {
      this.userGroups = users;
    });

    this.userService.getUsersWithGroups().subscribe({
      next: (data) => {
        this.userGroups = data;
        this.errorMessage = null;
      },
      error: (err) => {
        this.errorMessage = err.message;
      }
    });

    this.userService.updateUsers();
  }
}
