import { Component, Inject, OnInit } from '@angular/core';
import { ApiClient, Feature, TimeSpent } from 'src/app/api-clients/api-client';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EditTimeSpentComponent } from '../../time-sheets/edit-time-spent/edit-time-spent.component';

@Component({
  selector: 'app-feature-view',
  templateUrl: './feature-view.component.html',
  styleUrls: ['./feature-view.component.css']
})
export class FeatureViewComponent implements OnInit {

  feature!: Feature;
  showLoader: boolean = false;
  timeSpents: TimeSpent[] = [];

  constructor(protected snackBar: MatSnackBar,
    protected apiClient: ApiClient,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: { feature: Feature },
    public dialogRef: MatDialogRef<FeatureViewComponent>) { }

  editTimeSpent(timeSpentId: number) {
    let spent = this.timeSpents.filter(x => x.id = timeSpentId);
    const dialogTimeSpentRefRef = this.dialog.open(EditTimeSpentComponent, {
      width: '600px',
      data: { timeSpent: spent[0], featureId: this.feature.id, bugId: 0 }
    });

    dialogTimeSpentRefRef.afterClosed().subscribe(result => {
      result;
      if (result != null) {
        this.snackBar.open("Time spent saved", "Succes");
        this.loadTimeTrack();
      }
    });
  }

  openEditTimeSpentDialog() {
    const dialogTimeSpentRefRef = this.dialog.open(EditTimeSpentComponent, {
      width: '600px',
      data: { timeSpent: null, featureId: this.feature.id, bugId: 0 }
    });

    dialogTimeSpentRefRef.afterClosed().subscribe(result => {
      result;
      if (result != null) {
        this.snackBar.open("Time spent saved", "Succes");
        this.loadTimeTrack();
      }
    });
  }

  loadTimeTrack() {
    this.apiClient.getTimeSpentByFeature(this.feature.id).subscribe(data => {
      this.timeSpents = data;
    })
  }

  ngOnInit(): void {
    this.feature = this.data.feature;
    this.loadTimeTrack();
  }

}
