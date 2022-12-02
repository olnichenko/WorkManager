import { ChangeDetectorRef, Component, OnInit, OnDestroy } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ApiClient, UserVm } from '../../api-clients/api-client';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit, OnDestroy {
  public showLoader: boolean = false;
  public user: UserVm | null = new UserVm();
  userForm!: FormGroup;
  isUserExist: boolean = true;
  isTryLoggedIn: boolean = false;
  private subscriptions: Subscription[] = [];

  constructor(protected formBuilder: FormBuilder,
     protected snackBar: MatSnackBar, 
     protected accountService: AccountService, 
     protected router: Router) { }
  ngOnDestroy(): void {
    this.unsubscribe();
  }

  ngOnInit(): void {
    if (this.accountService.getCurrentUser() != null) {
      this.router.navigate([""]);
    }
    this.addSubscriptions()

    this.userForm = this.formBuilder.group({
      email: ["", [Validators.email, Validators.required]],
      password: ["", [Validators.required, Validators.minLength(6)]],
    })
  }

  public login(): void {
    this.isTryLoggedIn = true;
    this.showLoader = true;
    this.user = this.userForm.getRawValue();
    this.accountService.login(this.user?.email!, this.userForm.get('password')?.value);
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

  private addSubscriptions() {
    const userSubs = this.accountService.user.subscribe((data) => {
      if (!this.isTryLoggedIn) {
        return;
      }
      if (data == null) {
        this.openMessage("Email or Password is incorrect.", "Error");
      } else {
        this.user = data;
        this.openMessage("You have successfully logged in", "Success");
        this.router.navigate(["projects"]);
      }
      this.showLoader = false;
    });
    this.subscriptions.push(userSubs);
  }

  private unsubscribe() {
    this.subscriptions
      .forEach(s => s.unsubscribe());
  }
}
