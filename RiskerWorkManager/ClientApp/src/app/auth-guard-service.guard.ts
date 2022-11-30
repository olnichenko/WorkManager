import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from './services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardServiceGuard implements CanActivate {

  constructor(protected accountService: AccountService) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    let result: boolean = false;
    switch (route.routeConfig?.path) {
      case "projects":
        result = this.accountService.getCurrentUser() != null;
        break;
      case "project/:id":
        result = this.accountService.getCurrentUser() != null;
        break;
    }
    return result;
  }

}
