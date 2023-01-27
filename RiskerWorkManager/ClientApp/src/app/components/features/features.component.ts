import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { timeStamp } from 'console';
import { ApiClient, Feature, Project } from 'src/app/api-clients/api-client';
import { AccountService } from 'src/app/services/account.service';
import { ProjectService } from 'src/app/services/project.service';
import { runInThisContext } from 'vm';
import { AddFeatureComponent } from './add-feature/add-feature.component';

@Component({
  selector: 'app-features',
  templateUrl: './features.component.html',
  styleUrls: ['./features.component.css']
})
export class FeaturesComponent implements OnInit {

  selectedFeature!: Feature;
  project: Project = new Project();
  features: Feature[] = [];
  displayedColumns: string[] = ['title', 'userCreated', 'dateCreated', 'solvedInVersion'];
  isEnableEdit = false;

  constructor(protected apiClient: ApiClient, 
    public accountService: AccountService, 
    public dialog: MatDialog, 
    public projectSevice: ProjectService,
    protected snackBar: MatSnackBar){}

  ngOnInit(): void { 
    this.isEnableEdit = this.projectSevice.isUserCanEditProject();
    this.projectSevice.project.subscribe((data) => {
      this.project = data;
      this.loadFeatures();
    });
  }
  
  rowSelected(row: Feature){
    this.selectedFeature = row;
  }

  loadFeatures(): void{
    this.apiClient.getFeaturesByProject(this.project.id).subscribe((features) => {
      this.features = features;
    })
  }

  openNewDialog(): void {
    const dialogRef = this.dialog.open(AddFeatureComponent, {
      width: '800px',
      data:{feature: null, projectId: this.project.id}
    });

    dialogRef.afterClosed().subscribe(result => {
      let project = result;
      if (project != null) {
        this.snackBar.open("Feature saved", "Succes");
        this.loadFeatures();
      }
    });
  }

  openEditDialog(): void {
    if (this.selectedFeature == null){
      return;
    }
    const dialogRef = this.dialog.open(AddFeatureComponent, {
      width: '800px',
      data:{feature: this.selectedFeature, projectId: this.project.id}
    });

    dialogRef.afterClosed().subscribe(result => {
      let project = result;
      if (project != null) {
        this.snackBar.open("Feature saved", "Succes");
        this.loadFeatures();
      }
    });
  }

}
