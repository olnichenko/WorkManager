import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidationErrors, ValidatorFn, Validators } from '@angular/forms';
import { ApiClient, User } from '../../api-clients/api-client';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  public user: User = new User();
  //userForm = new FormGroup({
  //  email: new FormControl(),
  //  password: new FormControl(),
  //  confirmPassword: new FormControl()
  //});
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
    // alert(this.user.email + " " + this.user.password);
    this.apiClient.isEmailUse(this.user.email!).subscribe((data) => {
      alert(data);
    })
  }

  getErrorMessage(propertyName: string, errorName: string): string | void {
    this.userForm.get(propertyName)?.errors;
    return "You must enter a value";
  }

  checkPasswords: ValidatorFn = (userForm: AbstractControl): ValidationErrors | null => {
    let password = userForm.get('password')?.value;
    let confirmPassword = userForm.get('confirmPassword')?.value
    return password === confirmPassword ? null : { notSame: true }
  }
}
