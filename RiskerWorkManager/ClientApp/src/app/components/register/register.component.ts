import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ApiClient, User } from '../../api-clients/api-client';
import { ToastComponent } from '../toast/toast.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public isEmailUse: boolean = false;
  public showLoader: boolean = false;
  public user: User = new User();
  @ViewChild(ToastComponent) toast!: ToastComponent;
  userForm!: FormGroup;
  constructor(protected apiClient: ApiClient, protected formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      email: ["", [Validators.email, Validators.required]],
      password: ["", [Validators.required, Validators.minLength(6)]],
      confirmPassword: ["", [Validators.required, Validators.minLength(6)]]
    }, { validators: this.checkPasswords })
  }

  public register(): void {
    this.showLoader = true;
    this.toast.showToast("data loaded");
    this.user = this.userForm.getRawValue();
    this.apiClient.isEmailUse(this.user.email!).subscribe((data) => {
      this.isEmailUse = data;
      if (data) {
        this.showLoader = false;
        return;
      }
      this.apiClient.register(this.userForm.get('password')?.value, this.user).subscribe((data) => {
        if (data != null) {
          this.toast.showToast("data loaded");
        }
        this.showLoader = false;
      })
    })
  }

  getErrorMessage(propertyName: string): string | void {
    //let obj = this.userForm.get(propertyName)?.errors;
    //Object.entries(obj!).forEach(([key, value], index) => {
    //  console.log(key, value, index);
    //});
    //return "You must enter a value";
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
        //if (this.userForm.hasError('notEqualsPasswords')) {
        //  return 'Passwords is not equals';
        //}
       break;
    }
  }

  checkPasswords: ValidatorFn = (userForm: AbstractControl): ValidationErrors | null => {
    let password = userForm.get('password')?.value;
    let confirmPassword = userForm.get('confirmPassword')?.value
    return password === confirmPassword ? null : { notEqualsPasswords: true }
  }
}
