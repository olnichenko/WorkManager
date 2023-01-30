import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, TimeSpent } from 'src/app/api-clients/api-client';

@Component({
  selector: 'app-edit-time-spent',
  templateUrl: './edit-time-spent.component.html',
  styleUrls: ['./edit-time-spent.component.css']
})
export class EditTimeSpentComponent implements OnInit {
  timeSpentForm!: FormGroup;
  title: string = "";
  showLoader: boolean = false;
  timeSpent: TimeSpent | null = null;
  featureId!: number;
  bugId!: number;

  constructor(protected formBuilder: FormBuilder,
    protected snackBar: MatSnackBar,
    protected apiClient: ApiClient,
    @Inject(MAT_DIALOG_DATA) public data: { timeSpent: TimeSpent, featureId: number, bugId: number },
    public dialogRef: MatDialogRef<EditTimeSpentComponent>) { }

  ngOnInit(): void {
    this.timeSpent = this.data.timeSpent;
    this.featureId = this.data.featureId;
    this.bugId = this.data.bugId;
    this.title = this.timeSpent == null ? "Add new time spent" : "Edit time spent";

    this.timeSpentForm = this.formBuilder.group({
      comment: [this.timeSpent?.comment],
      hoursCount: [this.timeSpent?.hoursCount, [Validators.required]],
      dateFrom: [this.timeSpent?.dateFrom, [Validators.required]],
      id: [this.timeSpent == null ? 0 : this.timeSpent.id]
    })
  }

  public save() {
    this.showLoader = true;
    let timeSpent: TimeSpent = this.timeSpentForm.getRawValue();
    timeSpent.id = this.timeSpent == null ? 0 : this.timeSpent.id;
    this.apiClient.createOrUpdateTimeSpent(this.featureId, this.bugId, timeSpent).subscribe((data) => {
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
      case 'hoursCount':
        if (this.timeSpentForm.get(propertyName)?.hasError("required")) {
          return 'Hours count is required';
        }
        break;
      case 'dateFrom':
        if (this.timeSpentForm.get(propertyName)?.hasError("required")) {
          return 'Start date is required';
        }
        break;
    }
  }
}
