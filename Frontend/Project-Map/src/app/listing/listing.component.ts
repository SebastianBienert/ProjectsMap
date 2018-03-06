import { EmployeeService } from './../services/employee.service';
import { Component, OnInit } from '@angular/core';
import { Employee } from '../common-interfaces/employee';
import { HandleError } from '../services/http-error-handler.service';
import { SharedService } from '../services/shared.service';

@Component({
  selector: 'app-listing',
  templateUrl: './listing.component.html',
  providers: [ EmployeeService ],
  styleUrls: ['./listing.component.css']
})
export class ListingComponent implements OnInit {
  employees : Employee[];
  private handleError: HandleError;

  constructor(private employeeService : EmployeeService, private sharedService : SharedService) {
    
   }

  ngOnInit() {
    this.sharedService.employees.subscribe(x => this.employees = x);
  }


}



