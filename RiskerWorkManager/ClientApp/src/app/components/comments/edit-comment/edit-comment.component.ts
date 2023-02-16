import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, Comment } from 'src/app/api-clients/api-client';

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

  constructor(protected snackBar: MatSnackBar,
    protected formBuilder: FormBuilder,
    protected apiClient: ApiClient,
    @Inject(MAT_DIALOG_DATA) public data: { comment: Comment, featureId: number, bugId: number },
    public dialogRef: MatDialogRef<EditCommentComponent>) { }

  ngOnInit(): void {
    this.comment = this.data.comment;
    this.featureId = this.data.featureId;
    this.bugId = this.data.bugId;
    this.title = this.comment == null ? "Add new comment" : "Edit comment";

    this.commentForm = this.formBuilder.group({
      content: [this.comment?.content, [Validators.required]],
      id: [this.comment == null ? 0 : this.comment.id]
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
        this.dialogRef.close(data);
      } else {
        this.snackBar.open("Error", "Error")
      }
    });
  }
}
