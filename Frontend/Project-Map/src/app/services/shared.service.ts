
import { SearchType } from './../enums/SearchType';
import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { Employee } from './../common-interfaces/employee';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';


import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError } from 'rxjs/operators';
import { Subject } from 'rxjs/Subject';
import { Project } from '../common-interfaces/project';
import { EmployeeService } from './employee.service';


const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
    })
};

@Injectable()
export class SharedService {
    private handleError: HandleError;

    constructor(private employeeService: EmployeeService) {
        
    }

    private searchType : SearchType;
    private filter : string;

    private employeesSubject = new Subject<Employee[]>();
    private projectsSubject = new Subject<Project[]>();
    private page : number = 0;

    employees = this.employeesSubject.asObservable();
    projects = this.projectsSubject.asObservable();

    setFoundEmployees(val : Employee[]) {
        this.employeesSubject.next(val);
    }

    setFoundProjects(val : Project[]) {
        this.projectsSubject.next(val);
    }

    setSearchParameters(filter: string, searchType : SearchType){
        console.log("abubakar");
        this.page = 0;
        this.searchType = searchType;
        this.filter = filter;
    }

    loadChunkOfData(){
        this.employeeService.searchEmployeeByTechnology(this.filter, this.page)
          .subscribe(x => this.setFoundEmployees(x));
        this.page++;
        console.log("moze tu");
    }
}