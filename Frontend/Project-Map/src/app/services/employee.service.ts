import { SecurityService } from './../security/security.service';
import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { Employee } from './../common-interfaces/employee';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

import { ResponseContentType } from '@angular/http'
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError } from 'rxjs/operators';
import { Subject } from 'rxjs/Subject';
import { Globals } from './../globals';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'my-auth-token'
  })
};

@Injectable()
export class EmployeeService {
  employeeUrl: string;
  private handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler,
    private globals: Globals) {
    this.handleError = httpErrorHandler.createHandleError('EmployeeService');
    this.employeeUrl = globals.getUrl() + '/api/developers';
    this.searchedEmployees = this.getEmployees();
  }

  searchedEmployees: Observable<Employee[]> = new Observable<Employee[]>();

 public addEmployee(fileToUpload: File, employee : Employee)
  {
    this.http.post(this.employeeUrl, employee).subscribe(
      res => {
        console.log(res);
        if(fileToUpload != null )  //check if file is not empty
          this.uploadEmployeePhoto(fileToUpload, employee.Id);
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

  getEmployee(id: number): Observable<Employee> {
    return this.http.get<Employee>(this.employeeUrl + "/" + id)
      .pipe(
        catchError(this.handleError<Employee>('getEmployee'))
      );
  }

  searchEmployeeByTechnology(technology: string, page: number): Observable<any> {
    let params = new HttpParams({
      fromObject: {
        page: page.toString(),
        pageSize: "7"
      }
    });

    return this.searchedEmployees = this.http.get<any>(this.employeeUrl + "/technology/pagination/" + technology, { params })
      .pipe(
        catchError(this.handleError('getEmployees', []))
      );

  }

  searchEmployeeByName(name: string, page: number): Observable<any> {
    let params = new HttpParams({
      fromObject: {
        page: page.toString(),
        pageSize: "7"
      }
    });

    return this.searchedEmployees = this.http.get<any>(this.employeeUrl + "/pagination/" + name, { params })
      .pipe(
        catchError(this.handleError('getEmployees', []))
      );
  }

  public getEmployeesByQuery(query : string) : Observable<Employee[]>{
    return this.http.get<Employee[]>(this.employeeUrl + "/like/" + query)
    .pipe(
      catchError(this.handleError('getEmployees', [])));
  }

  uploadEmployeePhoto(fileToUpload: File, id: number) {
    let input = new FormData();
    input.append('file', fileToUpload, fileToUpload.name);
    const url = this.employeeUrl + "/photo/" + id;
    this.http.post(url, input).subscribe(
      res => {
        console.log(res);
      },
      err => {
        console.log("Error during uploading photo occured...");
      }
    );;
}

}