import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, Note } from 'src/app/api-clients/api-client';

@Component({
  selector: 'app-edit-note',
  templateUrl: './edit-note.component.html',
  styleUrls: ['./edit-note.component.css']
})
export class EditNoteComponent implements OnInit {

  noteForm!: FormGroup;
  title: string = "";
  public showLoader: boolean = false;
  note: Note | null = null;
  projectId!: number;

  constructor(protected formBuilder: FormBuilder,
    protected snackBar: MatSnackBar,
    protected apiClient: ApiClient,
    @Inject(MAT_DIALOG_DATA) public data: { note: Note, projectId: number },
    public dialogRef: MatDialogRef<EditNoteComponent>) { }

  public save() {
    this.showLoader = true;
    let note: Note = this.noteForm.getRawValue();
    note.id = this.note == null ? 0 : this.note.id;
    this.apiClient.createOrUpdateNote(this.projectId, note).subscribe((data) => {
      this.showLoader = false;
      if (data) {
        this.dialogRef.close(data);
      } else {
        this.snackBar.open("Error", "Error")
      }

    });
  }

  getErrorMessage(propertyName: string): string | void {
    switch (propertyName) {
      case 'title':
        if (this.noteForm.get(propertyName)?.hasError("required")) {
          return 'Name is required';
        }
        if (this.noteForm.get(propertyName)?.hasError("minlength")) {
          return 'Enter correct name';
        }
        break;
    }
  }

  ngOnInit(): void {
    this.note = this.data.note;
    this.projectId = this.data.projectId;
    this.title = this.note == null ? "Add new note" : "Edit note";

    this.noteForm = this.formBuilder.group({
      title: [this.note?.title, [Validators.required, Validators.minLength(3)]],
      content: [this.note?.content],
      id: [this.note == null ? 0 : this.note.id]
    })
  }
}
