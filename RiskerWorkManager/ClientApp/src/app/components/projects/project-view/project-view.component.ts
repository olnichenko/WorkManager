import { Component, Input, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ActivatedRoute, Router } from '@angular/router';
import { timeStamp } from 'console';
import { Subscription } from 'rxjs';
import { ApiClient, Project } from 'src/app/api-clients/api-client';
import { AccountService } from 'src/app/services/account.service';
import { ProjectService } from 'src/app/services/project.service';
import { AddUserToProjectComponent } from '../add-user-to-project/add-user-to-project.component';

@Component({
  selector: 'app-project-view',
  templateUrl: './project-view.component.html',
  styleUrls: ['./project-view.component.css']
})
export class ProjectViewComponent implements OnInit, OnDestroy  {

  isEnableEdit: boolean = false;
  project: Project = new Project();
  private subscriptions: Subscription[] = [];

  constructor(public projectSevice: ProjectService, 
    protected accountService: AccountService, 
    protected apiClient: ApiClient,
    private router: Router,
    public dialog: MatDialog, 
    protected snackBar: MatSnackBar){ }

    openAddUserDialog(): void {
      const dialogRef = this.dialog.open(AddUserToProjectComponent, {
        width: '600px'
      });
  
      dialogRef.afterClosed().subscribe(result => {
        if (result) {
          this.apiClient.addUserToProject(result, this.project.id).subscribe((data) => {
            if(data){
              this.snackBar.open("User added to project", "Succes");
              this.projectSevice.loadProject(this.project.id);
            }else{
              this.snackBar.open("Error", "Error");
            }
          })
        }
      });
    }

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
