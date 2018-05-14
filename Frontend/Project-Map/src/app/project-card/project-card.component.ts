import { Component, OnInit, Input } from '@angular/core';
import { Project } from '../common-interfaces/project';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-card',
  templateUrl: './project-card.component.html',
  styleUrls: ['./project-card.component.css']
})
export class ProjectCardComponent implements OnInit {

  @Input() project : Project;
  constructor(public router: Router) { }

  ngOnInit() {
  }

  showMore(){
    this.router.navigate(['/main',{outlets: {right: ['project', this.project.Id]} }]);
  }
}
