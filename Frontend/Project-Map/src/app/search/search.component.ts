import { Component, OnInit } from '@angular/core';
import { EmployeeService } from './../services/employee.service';
import { ProjectService } from './../services/project.service';
import { Employee } from '../common-interfaces/employee';
import { HandleError } from '../services/http-error-handler.service';
import { SharedService } from '../services/shared.service';


@Component({
  selector: 'app-search',
  templateUrl: './search.component.html',
  providers: [EmployeeService, ProjectService],
  styleUrls: ['./search.component.css']
})
export class SearchComponent implements OnInit {

  employees: Employee[];
  private handleError: HandleError;
  filter: string;

  constructor(private employeeService: EmployeeService, projectService : ProjectService, private sharedService: SharedService) { }

  search(): void {
    var isEmp: boolean = false;

    if (isEmp) {
      this.employeeService.searchEmployeeByTechnology(this.filter)
        .subscribe(x => this.sharedService.setFoundEmployees(x));
    }
    else {
      this.projectService.
    }

  }

  ngOnInit() {
  }

}
