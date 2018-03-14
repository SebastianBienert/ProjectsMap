import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { Employee } from './../common-interfaces/employee';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
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
export class EmployeeService {
  employeeUrl = 'http://localhost:58923/api/developers';  // For localhosted webapi
 // employeeUrl = 'https://projectsmapwebapi.azurewebsites.net/api/developers';  // For localhosted webapi
  private handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('EmployeeService');

    this.searchedEmployees = this.getEmployees();
  }
  
  searchedEmployees : Observable<Employee[]> = new Observable<Employee[]>();
  
 public addEmployee(employee : Employee)
  {
    this.http.post(this.employeeUrl, employee).subscribe(
      res => {
        console.log(res);
      },
      err => {
        console.log("Error occured");
      }
    );
  }



  /** GET Employees from the server */
  getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.employeeUrl)
      .pipe(
        catchError(this.handleError('getEmployees', []))
      );
  }

  searchEmployeeByTechnology(technology : string): Observable<Employee[]> {
    

    return this.searchedEmployees = this.http.get<Employee[]>(this.employeeUrl + "/technology/" + technology)
    .pipe(
      catchError(this.handleError('getEmployees', []))
    );
    
  }
}