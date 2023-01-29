import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, Bug, Version } from 'src/app/api-clients/api-client';

@Component({
  selector: 'app-edit-bug',
  templateUrl: './edit-bug.component.html',
  styleUrls: ['./edit-bug.component.css']
})
export class EditBugComponent implements OnInit {

  bugForm!: FormGroup;
  title: string = "";
  public showLoader: boolean = false;
  bug: Bug | null = null;
  projectId!: number;
  versions: Version[] = [];
  selectedVersion!: number;

  constructor(protected formBuilder: FormBuilder,
    protected snackBar: MatSnackBar,
    protected apiClient: ApiClient,
    @Inject(MAT_DIALOG_DATA) public data: { bug: Bug, projectId: number },
    public dialogRef: MatDialogRef<EditBugComponent>) { }

  public save() {
    this.showLoader = true;
    let bug: Bug = this.bugForm.getRawValue();
    bug.id = this.bug == null ? 0 : this.bug.id;
    this.apiClient.createOrUpdateBug(this.projectId, this.selectedVersion, bug).subscribe((data) => {
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
        if (this.bugForm.get(propertyName)?.hasError("required")) {
          return 'Name is required';
        }
        if (this.bugForm.get(propertyName)?.hasError("minlength")) {
          return 'Enter correct name';
        }
        break;
    }
  }

  ngOnInit(): void {
    this.bug = this.data.bug;
    this.projectId = this.data.projectId;
    this.title = this.bug == null ? "Add new bug" : "Edit bug";
    this.selectedVersion = this.bug?.solvedInVersion?.id;

    this.apiClient.getVersionsByProject(this.projectId).subscribe(data => {
      this.versions = data;
    })

    this.bugForm = this.formBuilder.group({
      title: [this.bug?.title, [Validators.required, Validators.minLength(3)]],
      content: [this.bug?.content],
      id: [this.bug == null ? 0 : this.bug.id],
      version: [this.selectedVersion]
    })
  }

}
