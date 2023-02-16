import { Component, Inject, OnInit } from '@angular/core';
import { ApiClient, Feature, TimeSpent, Comment } from 'src/app/api-clients/api-client';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EditTimeSpentComponent } from '../../time-sheets/edit-time-spent/edit-time-spent.component';
import { EditCommentComponent } from '../../comments/edit-comment/edit-comment.component';

@Component({
  selector: 'app-feature-view',
  templateUrl: './feature-view.component.html',
  styleUrls: ['./feature-view.component.css']
})
export class FeatureViewComponent implements OnInit {

  feature!: Feature;
  showLoader: boolean = false;
  timeSpents: TimeSpent[] = [];
  comments: Comment[] = [];

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

  openEditCommentDialog(commentId: number){
    let comment = this.comments.filter(x => x.id = commentId)[0];

    const dialogTimeSpentRefRef = this.dialog.open(EditCommentComponent, {
      width: '600px',
      data: { comment: comment, featureId: this.feature.id, bugId: 0 }
    });

    dialogTimeSpentRefRef.afterClosed().subscribe(result => {
      result;
      if (result != null) {
        this.snackBar.open("Data saved", "Succes");
        this.loadComments();
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

  loadComments(){
    this.apiClient.getCommentsByFeature(this.feature.id).subscribe(data => {
      this.comments = data;
    })
  }

  ngOnInit(): void {
    this.feature = this.data.feature;
    this.loadComments();
    this.loadTimeTrack();
  }

}
