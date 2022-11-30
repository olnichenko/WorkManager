import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { ApiClient, Project } from 'src/app/api-clients/api-client';
import { AccountService } from 'src/app/services/account.service';
import { AddProjectComponent } from './add-project/add-project.component';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {

  myProjects: Project[] = [];
  constructor(protected apiClient: ApiClient, 
    public accountService: AccountService, 
    public dialog: MatDialog, 
    protected snackBar: MatSnackBar,
    private router: Router){}

  ngOnInit(): void {
    this.loadProjects();
  }

  loadProjects(){
    this.apiClient.getMyProjects().subscribe((data) => {
      this.myProjects = data;
    })
  }

  navigateTo(projectId: number){
    this.router.navigate(['/project', projectId]);
  }

  openAddDialog(): void {
    const dialogRef = this.dialog.open(AddProjectComponent, {
      width: '800px'
    });

    dialogRef.afterClosed().subscribe(result => {
      let project = result;
      if (project != null) {
        this.snackBar.open("New project created", "Succes");
        this.loadProjects();
      }
    });
  }

}
