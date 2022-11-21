import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ApiClient, UserVm } from '../../api-clients/api-client';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public isEmailUse: boolean = false;
  public showLoader: boolean = false;
  public user: UserVm = new UserVm();

  userForm!: FormGroup;
  constructor(protected apiClient: ApiClient, protected formBuilder: FormBuilder, protected snackBar: MatSnackBar, protected router: Router) { }

  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      email: ["", [Validators.email, Validators.required]],
      password: ["", [Validators.required, Validators.minLength(6)]],
      confirmPassword: ["", [Validators.required, Validators.minLength(6)]],
      firstName: ["", [Validators.required, Validators.minLength(3)]],
      lastName: ["", [Validators.minLength(3)]]
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
          let snack = this.openMessage('You have successfully registered.', 'Success');
          snack.afterDismissed().subscribe(() => {
            this.router.navigate(['login']);
          });
        } else {
          this.openMessage('Registration failed.', 'Error');
        }
        this.showLoader = false;
      })
    })
  }

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
      case 'firstName':
        if (this.userForm.get(propertyName)?.hasError("required")) {
          return 'First name is required';
        }
        if (this.userForm.get(propertyName)?.hasError("minlength")) {
          return 'Enter correct first name';
        }
        break;
      case 'lastName':
        if (this.userForm.get(propertyName)?.hasError("minlength")) {
          return 'Enter correct last name';
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

  openMessage(message: string, action: string) {
    return this.snackBar.open(message, action);
  }
}
