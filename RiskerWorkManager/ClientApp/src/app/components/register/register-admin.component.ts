import { Component, OnInit } from "@angular/core";
import { Validators } from "@angular/forms";
//import { ApiClient, User } from "../../api-clients/api-client";
import { RegisterComponent } from "./register.component";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterAdminComponent extends RegisterComponent implements OnInit {
  ngOnInit(): void {
    this.apiClient.isAdminExist().subscribe((data) => {
      if (data) {
        let snack = this.openMessage("administrator already exists", "Error")
        snack.afterDismissed().subscribe(() => {
          this.router.navigate(['']);
        });
      }
    });

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
      this.apiClient.registerAdmin(this.userForm.get('password')?.value, this.user).subscribe((data) => {
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
}
