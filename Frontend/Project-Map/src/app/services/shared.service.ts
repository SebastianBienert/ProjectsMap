import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { Employee } from './../common-interfaces/employee';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';


import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError } from 'rxjs/operators';
import { Subject } from 'rxjs/Subject';
import { Project } from '../common-interfaces/project';


const httpOptions = {
    headers: new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': 'my-auth-token'
    })
};

@Injectable()
export class SharedService {
    private handleError: HandleError;

    constructor() {
        
    }

    private employeesSubject = new Subject<Employee[]>();
    private projectsSubject = new Subject<Project[]>();

    employees = this.employeesSubject.asObservable();
    projects = this.projectsSubject.asObservable();

    setFoundEmployees(val : Employee[]) {
        this.employeesSubject.next(val);
    }

    setFoundProjects(val : Project[]) {
        this.projectsSubject.next(val);
    }

}