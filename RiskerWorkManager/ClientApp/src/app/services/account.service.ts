import { Injectable } from '@angular/core';
import { UserVm } from '../api-clients/api-client';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  public testPermission: string = "Test permission";
  public user : UserVm | undefined;
  constructor() { }

  public isUserHaveAccess(permission: string): boolean {
    if (this.user == null) {
      return false;
    }
    if (this.user.isAdmin) {
      return true;
    }
    var result = this.user?.roles?.find(x => {
      return x.permissions?.find(y => y.name == permission);
    })
    if (result != null) {
      return true;
    }
    return false;
  }

  public isLoggedIn(): boolean {
    return this.user != null;
  }
}
