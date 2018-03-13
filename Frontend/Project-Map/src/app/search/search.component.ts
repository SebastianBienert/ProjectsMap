import { SearchType } from './../enums/SearchType';
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
  searchTypeText: string = "Pracownicy - nazwiska";
  selectedSearchType: SearchType = SearchType.employeeName;
  isEmp: boolean = false;

  constructor(private employeeService: EmployeeService, private projectService: ProjectService, private sharedService: SharedService) { }

  selectSearchType(selected: number, searchTypeText: string) {
    this.searchTypeText = searchTypeText;

    this.selectedSearchType = selected;
    console.log(this.selectedSearchType);
  }


  search(): void {

    switch (this.selectedSearchType) {

      // case SearchType.employeeName:
      //   this.employeeService.searchEmployeeByTechnology(this.filter)
      //     .subscribe(x => this.sharedService.setFoundEmployees(x));
      //   break;

      case SearchType.employeeTechnology:
        this.sharedService.setSearchParameters(this.filter, this.selectedSearchType);
        this.sharedService.loadChunkOfData();
        break;

      case SearchType.projectName:
        this.projectService.searchProjectByName(this.filter)
          .subscribe(x => this.sharedService.setFoundProjects(x));
        break;

      case SearchType.projectTechnology:
        this.projectService.searchProjectByName(this.filter)
          .subscribe(x => this.sharedService.setFoundProjects(x));
        break;
    }


    // this.isEmp = !this.isEmp;

    // if (this.isEmp) {
    //   this.employeeService.searchEmployeeByTechnology(this.filter)
    //     .subscribe(x => this.sharedService.setFoundEmployees(x));
    // }
    // else {
    //   this.projectService.searchProjectByName(this.filter)
    //     .subscribe(x => this.sharedService.setFoundProjects(x));
    // }

  }

  ngOnInit() {
  }

}
