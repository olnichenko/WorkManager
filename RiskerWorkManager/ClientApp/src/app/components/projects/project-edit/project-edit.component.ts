import { Component, OnDestroy, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { Project } from 'src/app/api-clients/api-client';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-project-edit',
  templateUrl: './project-edit.component.html',
  styleUrls: ['./project-edit.component.css']
})
export class ProjectEditComponent implements OnInit, OnDestroy  {

  project: Project = new Project;
  private subscriptions: Subscription[] = [];

  constructor(public projectService: ProjectService){}

  ngOnDestroy(): void {
    this.subscriptions
      .forEach(s => s.unsubscribe());
  }

  ngOnInit(): void {
    this.subscriptions.push(
      this.projectService.project.subscribe((data) => {
        this.project = data;
      })
    )
  }
}
