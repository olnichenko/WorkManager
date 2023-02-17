import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ApiClient, Project } from '../api-clients/api-client';
import { AccountService } from './account.service';

@Injectable()
export class ProjectService {

  private _project = new BehaviorSubject<Project>(new Project());
  readonly project = this._project.asObservable();
  private dataStore: { project: Project } = { project: new Project() };

  constructor(protected apiClient: ApiClient, protected accountService: AccountService) { }

  loadProject(id: number){
    this.apiClient.getProject(id).subscribe((data) => {
      this.dataStore.project = data;
      this._project.next(Object.assign({}, this.dataStore).project);
    })
  }

  isUserCanEditProject(){
    //return this.accountService.getCurrentUser()?.id == this.dataStore.project.userCreated?.id;
    return true;
  }
}
