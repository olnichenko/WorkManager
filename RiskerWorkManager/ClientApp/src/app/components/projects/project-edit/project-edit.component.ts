import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { ApiClient, Project } from 'src/app/api-clients/api-client';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.css']
})
export class ProjectEditComponent implements OnInit, OnDestroy  {

  project: Project = new Project;
  private subscriptions: Subscription[] = [];
  projectForm!: FormGroup;
  public showLoader: boolean = false;

  constructor(private router: Router, 
    public projectService: ProjectService, 
    protected formBuilder: FormBuilder, 
    protected apiClient: ApiClient, 
    protected snackBar: MatSnackBar){}

  public save() {
    this.showLoader = true;
    let project: Project = this.projectForm.getRawValue();
    this.apiClient.editProject(project).subscribe((data) => {
      if(data != null){
        this.snackBar.open("Project saved", "Succes");
      }else{
        this.snackBar.open("Error", "Error");
      }
      this.showLoader = false;
    });
  }

  public cancel(){
    this.router.navigate(['/project', this.project.id]);
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

  ngOnDestroy(): void {
    this.subscriptions
      .forEach(s => s.unsubscribe());
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.projectService.project.subscribe((data) => {
        this.project = data;
        this.projectForm = this.formBuilder.group({
          title: [this.project.title, [Validators.required, Validators.minLength(3)]],
          description: [this.project.description],
          content: [this.project.content],
          id: [this.project.id]
        })
      })
    )
  }
}
