import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ApiClient } from 'src/app/api-clients/api-client';

@Component({
  selector: 'app-add-user-to-project',
  templateUrl: './add-user-to-project.component.html',
  styleUrls: ['./add-user-to-project.component.css']
})
export class AddUserToProjectComponent implements OnInit {
  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      email: ["", [Validators.email, Validators.required]]
    })
  }

  public showLoader: boolean = false;
  userForm!: FormGroup;
  isUserExist: boolean = true;

  constructor(protected formBuilder: FormBuilder, protected apiClient: ApiClient, public dialogRef: MatDialogRef<AddUserToProjectComponent>) { }

  getErrorMessage(propertyName: string): string | void {
    switch (propertyName) {
      case 'email':
        if (this.userForm.get(propertyName)?.hasError("required")) {
          return 'E-mail is required';
        }
        if (this.userForm.get(propertyName)?.hasError("email")) {
          return 'Enter correct E-mail';
        }
        break;
    }
  }

  save(){
    this.showLoader = true;
    var email = this.userForm.get("email")?.value as string;
    this.apiClient.isEmailUse(email).subscribe((data) => {
      if(!data){
        this.isUserExist = false;
        this.showLoader = false;
        return;
      }else{
        this.showLoader = false;
        this.dialogRef.close(email);
      }
    })
  }
}
