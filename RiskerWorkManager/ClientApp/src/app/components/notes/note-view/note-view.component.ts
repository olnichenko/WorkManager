import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { ApiClient, Note, TimeSpent, Comment } from 'src/app/api-clients/api-client';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { EditTimeSpentComponent } from '../../time-sheets/edit-time-spent/edit-time-spent.component';
import { EditCommentComponent } from '../../comments/edit-comment/edit-comment.component';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpEventType, HttpRequest } from '@angular/common/http';

@Component({
  selector: 'app-note-view',
  templateUrl: './note-view.component.html',
  styleUrls: ['./note-view.component.css']
})
export class NoteViewComponent implements OnInit {

  note!: Note;
  showLoader: boolean = false;
  files: string[] = [];
  filePath: string = environment.apiUrl + "/Files/Notes/";
  @ViewChild('file')
  inputFiles!: ElementRef;

  constructor(
    protected snackBar: MatSnackBar,
    private http: HttpClient,
    protected apiClient: ApiClient,
    public dialog: MatDialog,
    @Inject(MAT_DIALOG_DATA) public data: { note: Note },
    public dialogRef: MatDialogRef<NoteViewComponent>
  ){}
  ngOnInit(): void {
    this.note = this.data.note;
    this.loadFiles();
  }

  upload(files: any) {
    if (files.length === 0)
      return;

    const formData = new FormData();

    for (const file of files) {
      formData.append(file.name, file);
    }

    const uploadReq = new HttpRequest('POST', environment.apiUrl + '/files/UploadToNote?noteId=' + this.note.id, formData, {
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

  viewFile(fileName: string){
    window.open(this.filePath + this.note.id + "/" + fileName, "_blank");
  }

  loadFiles(){
    this.apiClient.getNoteFiles(this.note.id).subscribe(data => {
      this.files = data;
    })
  }

  removeFile(name: string){
    this.apiClient.removeFileFromNote(name, this.note.id).subscribe(data => {
        this.snackBar.open("File deleted", "Succes");
        this.loadFiles();
    })
  }
}
