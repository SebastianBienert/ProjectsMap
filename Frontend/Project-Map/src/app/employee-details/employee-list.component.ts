// TODO SOMEDAY: Feature Componetized like CrisisCenter
import 'rxjs/add/operator/switchMap';
import { Observable } from 'rxjs/Observable';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, ParamMap } from '@angular/router';

import { Employee } from '../common-interfaces/employee';
import { EmployeeService }  from '../services/employee.service';

@Component({
  templateUrl: './employee-list.component.html',
  providers: [EmployeeService]
})
export class EmployeeListComponent implements OnInit {

  employee$: Employee[];

  private selectedId: number;

  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeeService
  ) {}

  ngOnInit() {
    this.employeeService.getEmployees()
      .subscribe(Employee => this.employee$ = Employee);
  }

}
