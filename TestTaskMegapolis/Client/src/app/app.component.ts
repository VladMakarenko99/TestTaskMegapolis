import { Component } from '@angular/core';
import { UserTableComponent } from './components/user-table/user-table.component';
import { GroupFormComponent } from './components/group-form/group-form.component';
import { UserFormComponent } from './components/user-form/user-form.component';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  standalone: true,
  imports: [UserTableComponent, GroupFormComponent, UserFormComponent]
})
export class AppComponent {

  title = 'Client';
}
