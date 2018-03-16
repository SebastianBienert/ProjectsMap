import { SearchType } from './../enums/SearchType';
import { Component, OnInit } from '@angular/core';
import { EmployeeService } from './../services/employee.service';
import { ProjectService } from './../services/project.service';
import { Employee } from '../common-interfaces/employee';
import { HandleError } from '../services/http-error-handler.service';
import { SharedService } from '../services/shared.service';
import { FormControl } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import 'rxjs/Rx';


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

  term = new FormControl();

  constructor(private employeeService: EmployeeService, private projectService: ProjectService, private sharedService: SharedService) {
    this.term.valueChanges
      .debounceTime(400)
      .distinctUntilChanged()
      .subscribe(x => {
        this.sharedService.setSearchParameters(x, this.selectedSearchType);
        this.sharedService.loadChunkOfData();
      });
    //.do(term => console.log("aaaa"));
  }

  selectSearchType(selected: number, searchTypeText: string) {
    this.searchTypeText = searchTypeText;

    this.selectedSearchType = selected;
  }


  search(): void {

    this.sharedService.setSearchParameters(this.filter, this.selectedSearchType);
    this.sharedService.loadChunkOfData();
  }

  ngOnInit() {
  }

}
