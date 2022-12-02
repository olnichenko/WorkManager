import { Component, Input } from '@angular/core';
import { NavigationCancellationCode, Router } from '@angular/router';

@Component({
  selector: 'app-project-nav-menu',
  templateUrl: './project-nav-menu.component.html',
  styleUrls: ['./project-nav-menu.component.css']
})
export class ProjectNavMenuComponent {

  @Input()projectId!: number;
  constructor(private router: Router){}

  navigateTo(path: string){
    this.router.navigate(['project', this.projectId, path]);
  }
}


