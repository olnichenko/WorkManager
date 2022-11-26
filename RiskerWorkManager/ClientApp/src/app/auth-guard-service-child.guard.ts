import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from './services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardServiceChildGuard implements CanActivate {
  constructor (protected accountService: AccountService){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree
  {
    let result: boolean = false;
    switch (route.routeConfig?.path) {
      case "roles":
        result = this.accountService.isUserHaveAccess(this.accountService.Roles_List);
        break;
      case "permissions":
        result = this.accountService.isUserHaveAccess(this.accountService.Permission_Edit);
        break;
    }
    return result;
  }
  
}
