import 'rxjs/add/operator/switchMap';
import { Component, OnInit, HostBinding } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';

import { Employee } from '../common-interfaces/employee';
import { EmployeeService }  from '../services/employee.service';

@Component({
  templateUrl: './employee-detail.component.html',
  styleUrls: ['./employee-detail.component.css']
})
export class EmployeeDetailComponent implements OnInit {

  employee$: Employee;
  photoUrl : string;
  photo : any;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private employeeService: EmployeeService
  ) {}

  ngOnInit() {
    this.route.paramMap
      .switchMap((params: ParamMap) =>
        this.employeeService.getEmployee(+params.get('id')))
          .subscribe(Employee => {
            this.employee$ = Employee
            this.employee$.PhotoUrl += "?q=XD" + new Date().getMilliseconds();
          });  
  }

  hideDetails() {
    this.router.navigate(['/']);
  }
}
