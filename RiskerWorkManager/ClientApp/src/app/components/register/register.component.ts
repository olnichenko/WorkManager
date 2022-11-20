import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ApiClient, User } from '../../api-clients/api-client';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public isEmailUse: boolean = false;
  public showLoader: boolean = false;
  public user: User = new User();
  public showMessage: boolean = false;
  public messageText!: string;
  public messageClass! :string;

  userForm!: FormGroup;
  constructor(protected apiClient: ApiClient, protected formBuilder: FormBuilder, protected matSnackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      email: ["", [Validators.email, Validators.required]],
      password: ["", [Validators.required, Validators.minLength(6)]],
      confirmPassword: ["", [Validators.required, Validators.minLength(6)]],
      firstName: ["", [Validators.required, Validators.minLength(3)]],
      lastName: [""]
    }, { validators: this.checkPasswords })
  }

  public register(): void {
    this.showLoader = true;
    this.user = this.userForm.getRawValue();
    this.apiClient.isEmailUse(this.user.email!).subscribe((data) => {
      this.isEmailUse = data;
      if (data) {
        this.showLoader = false;
        return;
      }
      this.apiClient.register(this.userForm.get('password')?.value, this.user).subscribe((data) => {
        if (data != null) {
          this.openMessage('You have successfully registered.', 'alert-success');
        } else {
          this.openMessage('Registration failed.', 'alert-danger');
        }
        this.showLoader = false;
      })
    })
  }

  getErrorMessage(propertyName: string): string | void {
    switch (propertyName) {
      case 'email':
        if (this.userForm.get(propertyName)?.hasError("required")) {
          return 'Email is required';
        }
        if (this.userForm.get(propertyName)?.hasError("email")) {
          return 'Enter correct email';
        }
        break;
      case 'password':
      case 'confirmPassword':
        if (this.userForm.get(propertyName)?.hasError("required")) {
          return 'Password is required';
        }
        if (this.userForm.get(propertyName)?.hasError("minlength")) {
          return 'Password min length 6';
        }
       break;
    }
  }

  checkPasswords: ValidatorFn = (userForm: AbstractControl): ValidationErrors | null => {
    let password = userForm.get('password')?.value;
    let confirmPassword = userForm.get('confirmPassword')?.value
    return password === confirmPassword ? null : { notEqualsPasswords: true }
  }

  openMessage(text: string, action: string) {
    this.showMessage = true;
    this.messageClass = action;
    this.messageText = text;
    let timer: ReturnType<typeof setTimeout> = setTimeout(() => { this.showMessage = false; }, 10000);
  }
}
