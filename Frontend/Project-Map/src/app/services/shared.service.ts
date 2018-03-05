import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { Employee } from './../common-interfaces/employee';
import { Injectable } from '@angular/core';
import { HttpHeaders } from '@angular/common/http';


import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError } from 'rxjs/operators';
import { Subject } from 'rxjs/Subject';


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

    private nameSource = new Subject<Employee[]>();

    employees = this.nameSource.asObservable();

    setFoundEmployees(val : Employee[]) {
        this.nameSource.next(val);
    }

}