import { ChangeDetectorRef, Component, OnInit, OnDestroy } from '@angular/core';
import { IsActiveMatchOptions, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { UserVm } from '../../api-clients/api-client';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent implements OnInit, OnDestroy {
  isExpanded = false;
  public user: UserVm | null | undefined;
  private subscriptions: Subscription[] = [];

  constructor(public accountService: AccountService, private cd: ChangeDetectorRef, private router: Router) { }
  ngOnInit(): void {
    this.addSubscriptions();
  }

  // projectLinkActive(): string{
  //   let matchOptions: IsActiveMatchOptions = {
  //     paths: 'subset',
  //     matrixParams: 'ignored',
  //     queryParams: 'ignored',
  //     fragment: 'ignored'
  // };
  //   return this.router.isActive('/', matchOptions) 
  //   || this.router.isActive('/projects', matchOptions)
  //   || this.router.isActive('/project', matchOptions) ? 'link-active' : '';
  // }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  logout() {
    this.accountService.logout();
    //this.cd.detectChanges();
  }

  private addSubscriptions() {
    const userSubs = this.accountService.user.subscribe((data) => {
      this.user = data;
      this.cd.detectChanges();
    });
    this.subscriptions.push(userSubs);
  }

  private unsubscribe() {
    this.subscriptions
      .forEach(s => s.unsubscribe());
  }

  ngOnDestroy(): void {
    this.unsubscribe();
  }
}
