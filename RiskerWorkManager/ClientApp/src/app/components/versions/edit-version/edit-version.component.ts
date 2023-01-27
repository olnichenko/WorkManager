import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { ApiClient, Version } from 'src/app/api-clients/api-client';

@Component({
  selector: 'app-edit-version',
  templateUrl: './edit-version.component.html',
  styleUrls: ['./edit-version.component.css']
})
export class EditVersionComponent implements OnInit {

  versionForm!: FormGroup;
  title: string = "";
  public showLoader: boolean = false;
  @Input() version: Version | null = null;
  @Input() projectId!: number;
  
  constructor(protected formBuilder: FormBuilder, 
    protected snackBar: MatSnackBar, 
    protected apiClient: ApiClient, 
    @Inject(MAT_DIALOG_DATA) public data: {version: Version, projectId: number},
    public dialogRef: MatDialogRef<EditVersionComponent>){ }

    public save() {
      this.showLoader = true;
      let version: Version = this.versionForm.getRawValue();
      version.id = this.version == null ? 0 : this.version.id;
      this.apiClient.createOrUpdateVersion(this.projectId, version).subscribe((data) => {
        this.showLoader = false;
        if(data){
          this.dialogRef.close(data);
        }else{
          this.snackBar.open("Error", "Error")
        }
        
      });
    }

    getErrorMessage(propertyName: string): string | void {
      switch (propertyName) {
        case 'title':
          if (this.versionForm.get(propertyName)?.hasError("required")) {
            return 'Name is required';
          }
          if (this.versionForm.get(propertyName)?.hasError("minlength")) {
            return 'Enter correct name';
          }
          break;
      }
    }

  ngOnInit(): void {
    this.version = this.data.version;
    this.projectId = this.data.projectId;
    this.title = this.version == null ? "Add new version" : "Edit version";

    this.versionForm = this.formBuilder.group({
      title: [this.version?.title, [Validators.required, Validators.minLength(3)]],
      content: [this.version?.content],
      id:[this.version == null ? 0 : this.version.id]
    })
  }

}
