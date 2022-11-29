import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';
import { ApiClient, UserVm } from '../api-clients/api-client';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  public readonly Users_List: string = "Users_List";
  public readonly Roles_List: string = "Roles_List";
  public readonly Role_Edit: string = "Role_Edit";
  public readonly Permission_Edit: string = "Permission_Edit";
  public readonly Logs_View: string = "Logs_View";

  private readonly _userKeyInStorage: string = "userKeyInStorage";
  public user: BehaviorSubject<UserVm | null> = new BehaviorSubject<UserVm | null>(null);
  constructor(private apiClient: ApiClient, protected router: Router) {
    let storageData = this.getCurrentUser();
    if (storageData?.token != null) {
      this.loingByToken(storageData.token);
    } else {
      localStorage.clear();
    }
  }

  public login(email: string, passwrod: string): void {
    this.apiClient.login(email, passwrod).subscribe((data) => {
      this.setCurrentUser(data);
      this.user.next(data);
    })
  }

  private loingByToken(token: string) {
    this.apiClient.loginByToken(token).subscribe((data) => {
      this.setCurrentUser(data);
      this.user.next(data);
    })
  }

  public getCurrentUser(): UserVm | undefined | null{
    if (this.user.getValue() == null) {
      let userStr = localStorage.getItem(this._userKeyInStorage);
      if (userStr != null) {
        let userData = JSON.parse(userStr);
        return userData;
      }
    }
    return this.user.getValue()
  }

  public logout() {
    this.apiClient.logout().subscribe();
    localStorage.clear();
    this.user.next(null);
    this.router.navigate([''])
  }

  private setCurrentUser(user: UserVm): void {
    localStorage.setItem(this._userKeyInStorage, JSON.stringify(user))
  }

  public isUserHaveAccess(permission: string): boolean {
    if (this.getCurrentUser() == null) {
      return false;
    }
    if (this.getCurrentUser()?.isAdmin) {
      return true;
    }
    var result = this.getCurrentUser()?.roles?.find(x => {
      return x.permissions?.find(y => y.name == permission);
    })
    if (result != null) {
      return true;
    }
    return false;
  }
}
