import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ApiClient, Project } from '../api-clients/api-client';

@Injectable()
export class ProjectService {

  private _project = new BehaviorSubject<Project>(new Project());
  readonly project = this._project.asObservable();
  private dataStore: { project: Project } = { project: new Project() };

  constructor(protected apiClient: ApiClient) { }

  loadProject(id: number){
    this.apiClient.getProject(id).subscribe((data) => {
      this.dataStore.project = data;
      this._project.next(Object.assign({}, this.dataStore).project);
    })
  }
}
