import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { ApiClient, Bug, TimeSpent, Comment } from 'src/app/api-clients/api-client';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EditTimeSpentComponent } from '../../time-sheets/edit-time-spent/edit-time-spent.component';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpEventType, HttpRequest } from '@angular/common/http';
import { EditCommentComponent } from '../../comments/edit-comment/edit-comment.component';

@Component({
  selector: 'app-bug-view',
  templateUrl: './bug-view.component.html',
  styleUrls: ['./bug-view.component.css']
})
export class BugViewComponent implements OnInit {

  bug!: Bug;
  showLoader: boolean = false;
  timeSpents: TimeSpent[] = [];
  comments: Comment[] = [];
  files: string[] = [];
  filePath: string = environment.apiUrl + "/Files/Bugs/";
  @ViewChild('file')
  inputFiles!: ElementRef;

  constructor(protected snackBar: MatSnackBar,
    private http: HttpClient,
    protected apiClient: ApiClient,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: { bug: Bug },
    public dialogRef: MatDialogRef<BugViewComponent>) { }

  loadComments() {
    this.apiClient.getCommentsByBug(this.bug.id).subscribe(data => {
      this.comments = data;
    })
  }

  openEditCommentDialog(commentId: number) {
    let comment = this.comments.filter(x => x.id = commentId)[0];

    const dialogTimeSpentRefRef = this.dialog.open(EditCommentComponent, {
      width: '600px',
      data: { comment: comment, featureId: 0, bugId: this.bug.id }
    });

    dialogTimeSpentRefRef.afterClosed().subscribe(result => {
      result;
      if (result != null) {
        this.snackBar.open("Data saved", "Succes");
        this.loadComments();
      }
    });
  }

  upload(files: any) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }

    const uploadReq = new HttpRequest('POST', environment.apiUrl + '/files/UploadToBug?bugId=' + this.bug.id, formData, {
      reportProgress: false,
    });

    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress) {

      };
      this.snackBar.open("File uploaded", "Succes");
      this.loadFiles();
    });
    this.inputFiles.nativeElement.value = "";
  }

  viewFile(fileName: string) {
    window.open(this.filePath + this.bug.id + "/" + fileName, "_blank");
  }

  loadFiles() {
    this.apiClient.getBugFiles(this.bug.id).subscribe(data => {
      this.files = data;
    })
  }

  removeFile(name: string) {
    this.apiClient.removeFileFromBug(name, this.bug.id).subscribe(data => {
      this.snackBar.open("File deleted", "Succes");
      this.loadFiles();
    })
  }

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
