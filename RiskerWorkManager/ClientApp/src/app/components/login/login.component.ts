import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, UserVm } from '../../api-clients/api-client';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  public showLoader: boolean = false;
  public user: UserVm = new UserVm();
  userForm!: FormGroup;
  isUserExist: boolean = true;

  constructor(protected apiClient: ApiClient, protected formBuilder: FormBuilder, protected snackBar: MatSnackBar, protected accountService: AccountService) { }

  ngOnInit(): void {
    this.userForm = this.formBuilder.group({
      email: ["", [Validators.email, Validators.required]],
      password: ["", [Validators.required, Validators.minLength(6)]],
    })
  }

  public test(): void {
    let str = "1234567890";
    this.apiClient.test(str).subscribe((data) => {
      this.openMessage(data, "test result");
    })
  }

  public login(): void {
    this.showLoader = true;
    this.user = this.userForm.getRawValue();
    this.apiClient.login(this.user.email!, this.userForm.get('password')?.value).subscribe((data) => {
      console.log(data);
      if (data == null) {
        this.isUserExist = false;
        this.openMessage("Email or Password is incorrect.", "Error");
      } else {
        this.isUserExist = true;
        this.accountService.user = data;
        this.openMessage("You have successfully logged in", "Success");
      }
      this.showLoader = false;
    });
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
      case 'password':
        if (this.userForm.get(propertyName)?.hasError("required")) {
          return 'Password is required';
        }
        if (this.userForm.get(propertyName)?.hasError("minlength")) {
          return 'Password min length 6';
        }
        break;
    }
  }

  openMessage(message: string, action: string) {
    this.snackBar.open(message, action);
  }
}
