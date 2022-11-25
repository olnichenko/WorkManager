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
    switch (route.routeConfig?.path) {
      case "roles":
        var result = this.accountService.isUserHaveAccess("test");
        break;
    
      default:
        break;
    }
    return true;
  }
  
}
