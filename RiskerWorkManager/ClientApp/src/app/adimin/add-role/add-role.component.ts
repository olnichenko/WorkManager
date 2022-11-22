import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ApiClient, Role } from '../../api-clients/api-client';

@Component({
  selector: 'app-add-role',
  templateUrl: './add-role.component.html',
  styleUrls: ['./add-role.component.css']
})
export class AddRoleComponent implements OnInit {
  roleForm!: FormGroup;
  public showLoader: boolean = false;
  public isRoleExist: boolean = false;

  constructor(protected formBuilder: FormBuilder, protected apiClient: ApiClient, public dialogRef: MatDialogRef<AddRoleComponent>) { }

  ngOnInit(): void {
    this.roleForm = this.formBuilder.group({
      name: ["", [Validators.required, Validators.minLength(3)]],
      description: [""],
    })
  }

  getErrorMessage(propertyName: string): string | void {
    switch (propertyName) {
      case 'name':
        if (this.roleForm.get(propertyName)?.hasError("required")) {
          return 'Name is required';
        }
        if (this.roleForm.get(propertyName)?.hasError("minlength")) {
          return 'Enter correct name';
        }
        break;
    }
  }

  public create() {
    this.showLoader = true;
    let role: Role = this.roleForm.getRawValue();
    this.apiClient.isRoleExist(role.name!).subscribe((data) => {
      if (data) {
        this.isRoleExist = true;
        this.showLoader = false;
        return;
      }
      this.apiClient.createRole(role).subscribe((data) => {
        this.showLoader = false;
        if (data.id > 0) {
          this.dialogRef.close(data);
        }
      })
    })
  }
}
