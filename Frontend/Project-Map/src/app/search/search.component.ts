import { Component, OnInit } from '@angular/core';
import { EmployeeService } from './../services/employee.service';
import { Employee } from '../common-interfaces/employee';
import { HandleError } from '../services/http-error-handler.service';
import { SharedService } from '../services/shared.service';


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  providers: [ EmployeeService ],
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  employees : Employee[];
  private handleError: HandleError;
  filter : string;
  
  constructor(private employeeService : EmployeeService, private sharedService : SharedService) { }

  search() : void{
    this.employeeService.searchEmployeeByTechnology(this.filter)
      .subscribe(x => this.sharedService.setFoundEmployees(x));
  }

  ngOnInit() {
  }

}
