import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule, FormArray } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { GroupService } from '../../services/group.service';
import { UserService } from '../../services/user.service';

interface Group {
  id: number;
  name: string;
}

@Component({
  selector: 'app-user-form',
  templateUrl: './user-form.component.html',
  styleUrls: ['./user-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, HttpClientModule]
})
export class UserFormComponent implements OnInit {
  userForm: FormGroup;
  groups: Group[] = [];
  submitted = false;
  successMessage = '';

  constructor(private formBuilder: FormBuilder, private groupService: GroupService, private userService: UserService) {
    this.userForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      selectedGroups: this.formBuilder.array([], Validators.required)
    });
  }

  ngOnInit(): void {
    this.groupService.groups$.subscribe(groups => {
      this.groups = groups;
    });
  }

  get selectedGroupsFormArray() {
    return this.userForm.get('selectedGroups') as FormArray;
  }

  onCheckboxChange(event: any) {
    const selectedGroups = this.selectedGroupsFormArray;
    if (event.target.checked) {
      selectedGroups.push(this.formBuilder.control(event.target.value));
    } else {
      const index = selectedGroups.controls.findIndex(x => x.value === event.target.value);
      selectedGroups.removeAt(index);
    }
  }

  get firstNameInvalid() {
    const control = this.userForm.get('firstName');
    return control?.invalid && (control?.dirty || control?.touched || this.submitted);
  }

  get lastNameInvalid() {
    const control = this.userForm.get('lastName');
    return control?.invalid && (control?.dirty || control?.touched || this.submitted);
  }

  onSubmit(): void {
    this.submitted = true;
    this.successMessage = '';

    if (this.userForm.invalid) {
      return;
    }

    const user = {
      firstName: this.userForm.value.firstName,
      lastName: this.userForm.value.lastName,
      groupIds: this.userForm.value.selectedGroups.map((id: string) => parseInt(id, 10))
    };

    this.userService.createUser(user).subscribe({
      next: () => {
        console.log('User added successfully');
        this.userForm.reset();
        this.submitted = false;
        this.successMessage = 'User added successfully!';
        this.userService.updateUsers(); 
      },
      error: (error) => {
        console.error('Error adding user:', error);
      }
    });
  }
}
