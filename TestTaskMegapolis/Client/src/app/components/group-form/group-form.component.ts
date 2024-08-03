import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { GroupService } from '../../services/group.service';


@Component({
  selector: 'app-group-form',
  templateUrl: './group-form.component.html',
  styleUrls: ['./group-form.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule]
})
export class GroupFormComponent {
  groupForm: FormGroup;
  submitted = false;
  successMessage = '';

  constructor(private formBuilder: FormBuilder, private groupService: GroupService) {
    this.groupForm = this.formBuilder.group({
      name: ['', Validators.required]
    });
  }

  get nameInvalid() {
    const control = this.groupForm.get('name');
    return control?.invalid && (control?.dirty || control?.touched || this.submitted);
  }

  onSubmit(): void {
    this.submitted = true;
    this.successMessage = '';

    if (this.groupForm.invalid) {
      return;
    }

    const groupName = this.groupForm.value.name;

    this.groupService.createGroup(groupName).subscribe({
      next: () => {
        this.groupForm.reset();
        this.submitted = false;
        this.successMessage = 'Group added successfully!';
        this.groupService.updateGroups();
      },
      error: (error) => {
        console.error('Error adding group:', error);
      }
    });
  }
}
