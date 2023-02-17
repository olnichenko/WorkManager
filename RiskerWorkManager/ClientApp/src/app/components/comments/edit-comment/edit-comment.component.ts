import { HttpClient, HttpEventType, HttpRequest } from '@angular/common/http';
import { Component, ElementRef, Inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, Comment } from 'src/app/api-clients/api-client';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-edit-comment',
  templateUrl: './edit-comment.component.html',
  styleUrls: ['./edit-comment.component.css']
})
export class EditCommentComponent implements OnInit {

  title: string = "";
  showLoader: boolean = false;
  comment: Comment | null = null;
  featureId!: number;
  bugId!: number;
  projectId!: number;
  commentForm!: FormGroup;
  files: string[] = [];
  filesToUplad: any;
  filePath: string = environment.apiUrl + "/Files/Comments/";
  @ViewChild('file')
  inputFiles!: ElementRef;

  constructor(protected snackBar: MatSnackBar,
    protected formBuilder: FormBuilder,
    private http: HttpClient,
    protected apiClient: ApiClient,
    @Inject(MAT_DIALOG_DATA) public data: { comment: Comment, featureId: number, bugId: number },
    public dialogRef: MatDialogRef<EditCommentComponent>) { }

  ngOnInit(): void {
    this.comment = this.data.comment;
    this.featureId = this.data.featureId;
    this.bugId = this.data.bugId;
    this.title = this.comment == null ? "Add new comment" : "Edit comment";
    if (this.comment != null) {
      this.loadFiles();
    }

    this.commentForm = this.formBuilder.group({
      content: [this.comment?.content, [Validators.required]],
      id: [this.comment == null ? 0 : this.comment.id]
    })
  }

  addFilesToUpload(files: any) {
    this.filesToUplad = files;
  }

  upload(colse: boolean) {
    if (this.filesToUplad.length === 0)
      return;

    const formData = new FormData();

    for (const file of this.filesToUplad) {
      formData.append(file.name, file);
    }

    const uploadReq = new HttpRequest('POST', environment.apiUrl + '/files/UploadToComment?commentId=' + this.comment?.id, formData, {
      reportProgress: false,
    });

    this.http.request(uploadReq).subscribe(event => {
      if (event.type === HttpEventType.UploadProgress) {

      };
      this.snackBar.open("File uploaded", "Succes");
      this.loadFiles();
      if (colse) {
        this.dialogRef.close(this.comment);
      }
    });
    this.inputFiles.nativeElement.value = "";
  }

  viewFile(fileName: string) {
    window.open(this.filePath + this.comment?.id + "/" + fileName, "_blank");
  }

  removeFile(name: string) {
    this.apiClient.removeFileFromComment(name, this.comment?.id).subscribe(data => {
      this.snackBar.open("File deleted", "Succes");
      this.loadFiles();
    })
  }

  loadFiles() {
    this.apiClient.getCommentFiles(this.comment?.id).subscribe(data => {
      this.files = data;
    })
  }

  getErrorMessage(propertyName: string): string | void {
    switch (propertyName) {
      case 'content':
        if (this.commentForm.get(propertyName)?.hasError("required")) {
          return 'Comment is required';
        }
        break;
    }
  }

  delete() {
    this.showLoader = true;
    let commentId = this.comment?.id;

    this.apiClient.deleteComment(commentId).subscribe(data => {
      this.showLoader = false;
      if (data) {
        this.dialogRef.close(data);
        this.snackBar.open("Data saved", "Succes")
      } else {
        this.snackBar.open("Error", "Error")
      }
    })
  }

  save() {
    this.showLoader = true;
    let comment: Comment = this.commentForm.getRawValue();
    comment.id = this.comment == null ? 0 : this.comment.id;

    this.apiClient.createOrUpdateComment(this.featureId, this.bugId, comment).subscribe(data => {
      this.showLoader = false;
      if (data) {
        this.comment = data;
        if (this.filesToUplad != null) {
          this.upload(true);
        } else {
          this.dialogRef.close(data);
        }
      } else {
        this.snackBar.open("Error", "Error")
      }
    });
  }
}
