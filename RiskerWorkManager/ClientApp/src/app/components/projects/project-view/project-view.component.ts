import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { timeStamp } from 'console';
import { Subscription } from 'rxjs';
import { ApiClient, Project } from 'src/app/api-clients/api-client';
import { AccountService } from 'src/app/services/account.service';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-project-view',
  templateUrl: './project-view.component.html',
  styleUrls: ['./project-view.component.css']
})
export class ProjectViewComponent implements OnInit, OnDestroy  {

  isEnableEdit: boolean = false;
  project: Project = new Project();
  private subscriptions: Subscription[] = [];

  constructor(public projectSevice: ProjectService, protected accountService: AccountService, private router: Router){ }

  ngOnDestroy(): void {
    this.unsubscribe();
  }

  navigateToEdit(){
    this.router.navigate(['/project/' + this.project.id + '/edit']);
  }

  private unsubscribe() {
    this.subscriptions
      .forEach(s => s.unsubscribe());
  }

  ngOnInit(): void {
    const sub =  this.projectSevice.project.subscribe((data) => {
      this.project = data;
      this.isEnableEdit = this.accountService.getCurrentUser()?.id == data.userCreated?.id;
    })
    this.subscriptions.push(sub);
  }
}
