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

  selectedFeature : Feature = new Feature();
  project: Project = new Project();
  features: Feature[] = []

  constructor(protected apiClient: ApiClient, 
    public accountService: AccountService, 
    public dialog: MatDialog, 
    public projectSevice: ProjectService,
    protected snackBar: MatSnackBar){}

  ngOnInit(): void {
    this.projectSevice.project.subscribe((data) => {
      this.project = data;
      this.apiClient.getFeaturesByProject(data.id).subscribe((features) => {
        this.features = features;
      })
    });
  }

  openEditDialog(): void {
    const dialogRef = this.dialog.open(AddFeatureComponent, {
      width: '800px',
      data:{feature: this.selectedFeature, projectId: this.project.id}
    });

    dialogRef.afterClosed().subscribe(result => {
      let project = result;
      if (project != null) {
        this.snackBar.open("Feature saved", "Succes");
      }
    });
  }

}
