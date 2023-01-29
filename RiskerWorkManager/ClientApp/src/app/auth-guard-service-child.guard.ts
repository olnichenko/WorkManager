import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from './services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardServiceChildGuard implements CanActivate {
  constructor(protected accountService: AccountService) { }
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let result: boolean = false;
    switch (route.routeConfig?.path) {
      case "roles":
        result = this.accountService.isUserHaveAccess(this.accountService.Roles_List);
        break;
      case "permissions":
        result = this.accountService.isUserHaveAccess(this.accountService.Permission_Edit);
        break;
      case "logs":
        result = this.accountService.isUserHaveAccess(this.accountService.Logs_View);
        break;
      case "users":
        result = this.accountService.isUserHaveAccess(this.accountService.Users_List);
        break;
      case "edit":
        result = this.accountService.getCurrentUser() != null;
        break;
      case "":
        result = this.accountService.getCurrentUser() != null;
        break;
      case "features":
        result = this.accountService.getCurrentUser() != null;
        break;
      case "versions":
        result = this.accountService.getCurrentUser() != null;
        break;
      case "bugs":
        result = this.accountService.getCurrentUser() != null;
        break;
      case "notes":
        result = this.accountService.getCurrentUser() != null;
        break;
      case "time-sheets":
        result = this.accountService.getCurrentUser() != null;
        break;
    }
    return result;
  }

}
