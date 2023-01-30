import { Component, Inject, OnInit } from '@angular/core';
import { ApiClient, Feature, TimeSpent } from 'src/app/api-clients/api-client';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EditTimeSpentComponent } from './edit-time-spent/edit-time-spent.component';

@Component({
  selector: 'app-time-sheets',
  templateUrl: './time-sheets.component.html',
  styleUrls: ['./time-sheets.component.css']
})
export class TimeSheetsComponent {

  
  showLoader: boolean = false;

  constructor(protected snackBar: MatSnackBar,
    protected apiClient: ApiClient,
    public dialog: MatDialog) { }

  openEditTimeSpentDialog() {
    const dialogTimeSpentRefRef = this.dialog.open(EditTimeSpentComponent, {
      width: '600px',
      data:{timeSpent: null, featureId: 0, bugId: 0}
    });

    dialogTimeSpentRefRef.afterClosed().subscribe(result => {
      let project = result;
      if (project != null) {
        this.snackBar.open("Time spent saved", "Succes");
      }
    });
  }

}
