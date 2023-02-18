import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, Feature, Project, ProjectItemFilterVm, Version } from 'src/app/api-clients/api-client';
import { AccountService } from 'src/app/services/account.service';
import { ProjectService } from 'src/app/services/project.service';
import { FeatureViewComponent } from './feature-view/feature-view.component';
import { AddFeatureComponent } from './add-feature/add-feature.component';

@Component({
  selector: 'app-features',
  templateUrl: './features.component.html',
  styleUrls: ['./features.component.css']
})
export class FeaturesComponent implements OnInit {

  selectedFeature: Feature | null = null;
  project: Project = new Project();
  features: Feature[] = [];
  displayedColumns: string[] = ['title', 'userCreated', 'dateCreated', 'solvedInVersion'];
  isEnableEdit = false;
  filter: ProjectItemFilterVm = new ProjectItemFilterVm();
  versions: Version[] = [];

  constructor(protected apiClient: ApiClient,
    public accountService: AccountService,
    public dialog: MatDialog,
    public projectSevice: ProjectService,
    protected snackBar: MatSnackBar) { }

  ngOnInit(): void {
    this.isEnableEdit = this.projectSevice.isUserCanEditProject();

    this.projectSevice.project.subscribe((data) => {
      this.project = data;
      this.filter.projectId = this.project.id;
      this.filter.startDateFrom = new Date();
      this.filter.startDateFrom.setDate(new Date().getDate() + -7);
      this.filter.endDateFrom = new Date();
      this.loadFeatures();
      this.apiClient.getVersionsByProject(this.project.id).subscribe(data => {
        this.versions = data;
      })
    });
  }

  confirmDelete() {
    var result = confirm("Are you sure you want to delete feature?");
    if (result) {
      this.apiClient.deleteFeature(this.selectedFeature?.id).subscribe(data => {
        if (data) {
          this.snackBar.open("Feature deleted", "Succes");
          this.loadFeatures();
        } else {
          this.snackBar.open("Error", "Error");
        }
      })
    }
  }

  rowSelected(row: Feature) {
    this.selectedFeature = row;
  }

  rowDblClick(row: Feature) {
    let item = this.features.find(x => x.id == row.id);
    const dialogRef = this.dialog.open(FeatureViewComponent, {
      width: '600px',
      data: { feature: item }
    });
  }

  loadFeatures(): void {
    this.apiClient.getFeaturesByFilter(this.filter).subscribe((features) => {
      this.features = features;
      this.selectedFeature = null;
    })
  }

  openNewDialog(): void {
    const dialogRef = this.dialog.open(AddFeatureComponent, {
      width: '800px',
      data: { feature: null, projectId: this.project.id }
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
    if (this.selectedFeature == null) {
      return;
    }
    const dialogRef = this.dialog.open(AddFeatureComponent, {
      width: '800px',
      data: { feature: this.selectedFeature, projectId: this.project.id }
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
