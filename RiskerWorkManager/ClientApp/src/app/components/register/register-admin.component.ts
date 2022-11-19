import { Component, OnInit } from "@angular/core";
import { ApiClient, User } from "../../api-clients/api-client";
import { RegisterComponent } from "./register.component";

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterAdminComponent extends RegisterComponent implements OnInit {
  ngOnInit(): void {
    this.apiClient.isAdminExist().subscribe((data) => {
      alert("admin is exist - " + data);
    })
  }

  public register(): void {
    this.apiClient.isEmailUse(this.user.email!).subscribe((data) => {
      alert(data);
    })
  }
}
