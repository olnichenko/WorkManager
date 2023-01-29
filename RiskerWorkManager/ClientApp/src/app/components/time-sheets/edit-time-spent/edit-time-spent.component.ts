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
export class EditTimeSpentComponent {
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
}
