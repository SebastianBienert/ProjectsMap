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
  getEmployeeLocationInfo(id: number): Observable<any> {
    return this.http.get<any>(this.employeeUrl + "/" + id + "/locationInfo")
    .pipe(
      catchError(this.handleError<Employee>('getEmployeeLocationInfo'))
    );
  }
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
        if(fileToUpload != null ) 
          this.uploadEmployeePhoto(fileToUpload, employee.Id);

      },
      err => {
        console.log(err);
      }
    );
  }

  
  editEmployee(fileToUpload: File, employee : Employee) : Observable<any>
  {
    return this.http.put<any>(this.employeeUrl + "/edit", employee).map(
      res => {
        console.log(res);
        if(fileToUpload != null )
        {
          this.deleteEmployeePhoto(employee.Id).subscribe(result =>{
          this.uploadEmployeePhoto(fileToUpload, employee.Id);
          });
        }
        return Observable.empty();
      },
      err => {
        console.log(err);
      }
    );
    //return Observable.empty();
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

  getCurrentUserEmployeeData(){
    return this.http.get<Employee>(this.employeeUrl + "/myInfo")
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

  getEmployeesByName(name: string): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.employeeUrl + "/" + name);
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

  deleteEmployeePhoto(id: number): Observable<any>{
    return this.searchedEmployees = this.http.delete<any>(this.employeeUrl + "/photo/" + id)
    .pipe(
      catchError(this.handleError('getEmployees', []))
    );
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