import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { timeStamp } from 'console';
import { ApiClient, Feature } from 'src/app/api-clients/api-client';

@Component({
  selector: 'app-add-feature',
  templateUrl: './add-feature.component.html',
  styleUrls: ['./add-feature.component.css']
})
export class AddFeatureComponent implements OnInit {

  featureForm!: FormGroup;
  public showLoader: boolean = false;
  @Input() feature: Feature | null = null;
  @Input() projectId!: number;
  
  constructor(protected formBuilder: FormBuilder, 
    protected snackBar: MatSnackBar, 
    protected apiClient: ApiClient, 
    @Inject(MAT_DIALOG_DATA) public data: {feature: Feature, projectId: number},
    public dialogRef: MatDialogRef<AddFeatureComponent>) { }

  ngOnInit(): void {
    this.feature = this.data.feature;
    this.projectId = this.data.projectId;

    this.featureForm = this.formBuilder.group({
      title: [this.feature?.title, [Validators.required, Validators.minLength(3)]],
      content: [this.feature?.content],
      id:[this.feature == null ? 0 : this.feature.id]
    })
  }

  getErrorMessage(propertyName: string): string | void {
    switch (propertyName) {
      case 'title':
        if (this.featureForm.get(propertyName)?.hasError("required")) {
          return 'Name is required';
        }
        if (this.featureForm.get(propertyName)?.hasError("minlength")) {
          return 'Enter correct name';
        }
        break;
    }
  }

  public save() {
    this.showLoader = true;
    let feature: Feature = this.featureForm.getRawValue();
    feature.id = this.feature == null ? 0 : this.feature.id;
    this.apiClient.createOrUpdateFeature(this.projectId, feature).subscribe((data) => {
      this.showLoader = false;
      if(data){
        this.dialogRef.close(data);
      }else{
        this.snackBar.open("Error", "Error")
      }
      
    });

  }
}
