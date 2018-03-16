import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { Project } from './../common-interfaces/project';
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
export class ProjectService {
  projectUrl = 'http://localhost:58923/api/project';  // For localhosted webapi
 // projectUrl = 'https://projectsmapwebapi.azurewebsites.net/api/developers';  // For localhosted webapi
  private handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('ProjectService');

  }
  
  searchProjectByName(name : string): Observable<Project[]> {
    
    return this.http.get<Project[]>(this.projectUrl + "/name/" + name)
    .pipe(
      catchError(this.handleError('getProjects', []))
    );
    
  }


  searchSetOfProjectsByName(name: string, page: number): Observable<Project[]> {
    let params = new HttpParams({
      fromObject: {
        page: page.toString(),
        pageSize: "7"
      }
    });

    return this.http.get<Project[]>(this.projectUrl + "/pagination/" + name, { params })
      .pipe(
        catchError(this.handleError('getEmployees', []))
      );

  }
}