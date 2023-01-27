import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, Project, Version } from 'src/app/api-clients/api-client';
import { AccountService } from 'src/app/services/account.service';
import { ProjectService } from 'src/app/services/project.service';
import { EditVersionComponent } from './edit-version/edit-version.component';

@Component({
  selector: 'app-versions',
  templateUrl: './versions.component.html',
  styleUrls: ['./versions.component.css']
})
export class VersionsComponent implements OnInit {
  selectedVersion!: Version;
  project: Project = new Project();
  versions: Version[] = [];
  isEnableEdit = false;
  displayedColumns: string[] = ['title', 'userCreated', 'dateCreated'];

  constructor(protected apiClient: ApiClient, 
    public accountService: AccountService, 
    public dialog: MatDialog, 
    public projectSevice: ProjectService,
    protected snackBar: MatSnackBar){}

  ngOnInit(): void { 
      this.isEnableEdit = this.projectSevice.isUserCanEditProject();
      this.projectSevice.project.subscribe((data) => {
        this.project = data;
        this.loadVersions();
    });
  }

  openEditDialog(){
    if (this.selectedVersion == null){
      return;
    }
    const dialogRef = this.dialog.open(EditVersionComponent, {
      width: '800px',
      data:{version: this.selectedVersion, projectId: this.project.id}
    });

    dialogRef.afterClosed().subscribe(result => {
      let project = result;
      if (project != null) {
        this.snackBar.open("Version saved", "Succes");
        this.loadVersions();
      }
    });
  }

  openNewDialog(){
    const dialogRef = this.dialog.open(EditVersionComponent, {
      width: '800px',
      data:{feature: null, projectId: this.project.id}
    });

    dialogRef.afterClosed().subscribe(result => {
      let project = result;
      if (project != null) {
        this.snackBar.open("Version saved", "Succes");
        this.loadVersions();
      }
    });
  }

  rowSelected(row: Version){
    this.selectedVersion = row;
  }

  loadVersions(){
    this.apiClient.getVersionsByProject(this.project.id).subscribe(data => {
      this.versions = data;
    })
  }
}
