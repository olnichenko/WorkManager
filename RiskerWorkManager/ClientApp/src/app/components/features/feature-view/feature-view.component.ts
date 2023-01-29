import { Component, Inject, OnInit } from '@angular/core';
import { ApiClient, Feature } from 'src/app/api-clients/api-client';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-feature-view',
  templateUrl: './feature-view.component.html',
  styleUrls: ['./feature-view.component.css']
})
export class FeatureViewComponent implements OnInit {

  feature!: Feature;
  showLoader: boolean = false;

  constructor(protected snackBar: MatSnackBar, 
    protected apiClient: ApiClient, 
    @Inject(MAT_DIALOG_DATA) public data: {feature: Feature},
    public dialogRef: MatDialogRef<FeatureViewComponent>) { }

  ngOnInit(): void {
    this.feature = this.data.feature;
  }
  
}
