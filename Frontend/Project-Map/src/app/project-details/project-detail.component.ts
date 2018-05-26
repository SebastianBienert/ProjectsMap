import 'rxjs/add/operator/switchMap';
import { Component, OnInit, HostBinding } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { Project } from '../common-interfaces/project';
import { Employee } from '../common-interfaces/employee';
import { ProjectService }  from '../services/project.service';
import { EmployeeService }  from '../services/employee.service';

@Component({
  templateUrl: './project-detail.component.html',
  styleUrls: ['./project-detail.component.css']
})
export class ProjectDetailComponent implements OnInit {

  project$: Project;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private projectService: ProjectService,
    private employeeService: EmployeeService
  ) {}

  ngOnInit() {
    this.route.paramMap
      .switchMap((params: ParamMap) =>
        this.projectService.getProject(+params.get('id')))
          .subscribe(Project => {
            this.project$ = Project
          });  
  }
  
  findUser(id: number): void {
    //let users: Employee[];
    this.router.navigate(['/main',{outlets: {right: ['user', id], center: [id]} }]);
   // this.employeeService.getEmployeesByName(user)
    //  .subscribe(emp => { this.router.navigate(['/main',{outlets: {right: ['user', emp[0].Id], center: [emp[0].Id]} }]) });
  }

  hideDetails() {
    this.router.navigate(['/']);
  }

  editProject(id: number): void{
    this.router.navigate(['/editProject'], {})
  }

}
