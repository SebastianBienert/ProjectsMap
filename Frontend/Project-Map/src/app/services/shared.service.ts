
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
import { ProjectService } from './project.service';


const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
    })
};

@Injectable()
export class SharedService {
    private handleError: HandleError;

    constructor(private employeeService: EmployeeService, private projectService: ProjectService) {

    }

    private emps: Employee[] = [];
    private projs: Project[] = [];

    private employeesSubject = new Subject<Employee[]>();
    private projectsSubject = new Subject<Project[]>();
    private listingOpen = new Subject<boolean>();

    private page: number = 0;

    employeesObservable = this.employeesSubject.asObservable();
    projectsObservable = this.projectsSubject.asObservable();
    listingState = this.listingOpen.asObservable();

    searchType: SearchType;
    filter: string;

    private setFoundEmployees(val: Employee[]) {
        console.log(val);
        val.forEach(element => {
           this.emps.push(element); 
        });
        this.employeesSubject.next(this.emps);
    }

    private setFoundProjects(val: Project[]) {
        val.forEach(element => {
            this.projs.push(element); 
         });
        this.projectsSubject.next(this.projs);
    }

    setListingState(isOpen : boolean)
    {
        this.listingOpen.next(isOpen);
    }

    setSearchParameters(filter: string, searchType: SearchType) {
        this.emps = [];
        this.projs = [];
        this.page = 0;
        this.searchType = searchType;
        this.filter = filter;
    }

    loadChunkOfData() {
        switch (this.searchType) {

            case SearchType.employeeName:
                this.employeeService.searchEmployeeByName(this.filter, this.page)
                    .subscribe(x => 
                    {
                        console.log(x);
                        console.log(x.result);
                        this.setFoundEmployees(x.Result);
                    }); 
                break;

            case SearchType.employeeTechnology:
                this.employeeService.searchEmployeeByTechnology(this.filter, this.page)
                    .subscribe(x => this.setFoundEmployees(x.Result));
                break;

            case SearchType.projectName:
                this.projectService.searchSetOfProjectsByName(this.filter, this.page)
                    .subscribe(x => this.setFoundProjects(x.Result));
                break;

            case SearchType.projectTechnology:
                this.projectService.searchSetOfProjectsByTechnology(this.filter, this.page)
                    .subscribe(x => this.setFoundProjects(x.Result));
                break;

        }

        this.page++;
    }
}