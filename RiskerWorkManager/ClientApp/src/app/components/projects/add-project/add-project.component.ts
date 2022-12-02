import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ApiClient, Project } from 'src/app/api-clients/api-client';

@Component({
  selector: 'app-add-project',
  templateUrl: './add-project.component.html',
  styleUrls: ['./add-project.component.css']
})
export class AddProjectComponent implements OnInit {
  projectForm!: FormGroup;
  public showLoader: boolean = false;

  constructor(protected formBuilder: FormBuilder, protected apiClient: ApiClient, public dialogRef: MatDialogRef<AddProjectComponent>) { }

  ngOnInit(): void {
    this.projectForm = this.formBuilder.group({
      title: ["", [Validators.required, Validators.minLength(3)]],
      description: [""],
    })
  }

  getErrorMessage(propertyName: string): string | void {
    switch (propertyName) {
      case 'title':
        if (this.projectForm.get(propertyName)?.hasError("required")) {
          return 'Title is required';
        }
        if (this.projectForm.get(propertyName)?.hasError("minlength")) {
          return 'Enter correct title';
        }
        break;
    }
  }

  public create() {
    this.showLoader = true;
    let project: Project = this.projectForm.getRawValue();
    this.apiClient.createProject(project).subscribe((data) => {
      this.showLoader = false;
      if (data.id > 0) {
        this.dialogRef.close(data);
      }
    })
  }
}
