import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-nav-menu',
  templateUrl: './project-nav-menu.component.html',
  styleUrls: ['./project-nav-menu.component.css']
})
export class ProjectNavMenuComponent implements OnInit {

  @Input()projectId!: number;
  constructor(private router: Router){}
  ngOnInit(): void {
  }

  navigateTo(path: string){
    this.router.navigate(['project', this.projectId, path]);
  }
}


