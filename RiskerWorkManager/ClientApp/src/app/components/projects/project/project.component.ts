import { Component, OnDestroy } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { ApiClient, Project } from 'src/app/api-clients/api-client';
import { ProjectService } from 'src/app/services/project.service';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css'],
  providers: [ProjectService]
})
export class ProjectComponent implements OnDestroy {

  project: Project = new Project();
  private subscriptions: Subscription[] = [];

  constructor(private route: ActivatedRoute, public projectService: ProjectService){
    var subPr = this.projectService.project.subscribe((data) => {
      this.project = data;
    })

    this.subscriptions.push(subPr);

    var subUrl = this.route.params.subscribe((params)=>{
      let id : number = params['id'];
      if (id > 0) {
        this.projectService.loadProject(id);
      }
    });

    this.subscriptions.push(subUrl);
  }
  ngOnDestroy(): void {
    this.subscriptions
      .forEach(s => s.unsubscribe());
  }
}
