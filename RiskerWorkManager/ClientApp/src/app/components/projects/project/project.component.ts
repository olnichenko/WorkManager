import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-project',
  templateUrl: './project.component.html',
  styleUrls: ['./project.component.css']
})
export class ProjectComponent {

  id!: number;

  constructor(private route: ActivatedRoute){
    route.params.subscribe(params=>this.id=params['id']);
  }
}
