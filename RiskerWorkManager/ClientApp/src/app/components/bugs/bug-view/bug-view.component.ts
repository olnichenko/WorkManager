import { Component, Inject, OnInit } from '@angular/core';
import { ApiClient, Bug, TimeSpent } from 'src/app/api-clients/api-client';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EditTimeSpentComponent } from '../../time-sheets/edit-time-spent/edit-time-spent.component';

@Component({
  selector: 'app-bug-view',
  templateUrl: './bug-view.component.html',
  styleUrls: ['./bug-view.component.css']
})
export class BugViewComponent implements OnInit {

  bug!: Bug;
  showLoader: boolean = false;
  timeSpents: TimeSpent[] = [];

  constructor(protected snackBar: MatSnackBar,
    protected apiClient: ApiClient,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: { bug: Bug },
    public dialogRef: MatDialogRef<BugViewComponent>) { }

  openEditTimeSpentDialog() {
    const dialogTimeSpentRefRef = this.dialog.open(EditTimeSpentComponent, {
      width: '600px',
      data: { timeSpent: null, featureId: 0, bugId: this.bug.id }
    });

    dialogTimeSpentRefRef.afterClosed().subscribe(result => {
      result;
      if (result != null) {
        this.snackBar.open("Time spent saved", "Succes");
        this.loadTimeTrack();
      }
    });
  }

  editTimeSpent(timeSpentId: number) {
    let spent = this.timeSpents.filter(x => x.id = timeSpentId);
    const dialogTimeSpentRefRef = this.dialog.open(EditTimeSpentComponent, {
      width: '600px',
      data: { timeSpent: spent[0], featureId: 0, bugId: this.bug.id }
    });

    dialogTimeSpentRefRef.afterClosed().subscribe(result => {
      result;
      if (result != null) {
        this.snackBar.open("Time spent saved", "Succes");
        this.loadTimeTrack();
      }
    });
  }

  ngOnInit(): void {
    this.bug = this.data.bug;
    this.loadTimeTrack();
  }

  loadTimeTrack() {
    this.apiClient.getTimeSpentByBug(this.bug.id).subscribe(data => {
      this.timeSpents = data;
    })
  }
}
