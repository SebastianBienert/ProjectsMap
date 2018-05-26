import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { Project } from './../common-interfaces/project';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Globals } from './../globals';


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
  projectUrl: string;
  private handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler,
    private globals: Globals) {
    this.handleError = httpErrorHandler.createHandleError('ProjectService');
    this.projectUrl = globals.getUrl() + '/api/project';
  }
   addProject(project : any): Observable<any>{
      return this.http.post<any>(this.projectUrl, project).
      pipe(catchError(this.handleError<any>('postProject',[])));
    }

    editProject(project : any) : Observable<any>{
      return this.http.put<any>(this.projectUrl + "/" + project.Id, project).
      pipe(catchError(this.handleError<any>('editProject',[])));
    }
  
  searchProjectByName(name : string): Observable<Project[]> {
    
    return this.http.get<Project[]>(this.projectUrl + "/name/" + name)
    .pipe(
      catchError(this.handleError('getProjects', []))
    );
    
  }

  delete(id : number) : Observable<any>{
    return this.http.delete<any>(this.projectUrl + "/" + id)
      .pipe(catchError(this.handleError<any>('deleteProject',[])));
  }
  
  getProject(id : number): Observable<Project> {
    
    return this.http.get<Project>(this.projectUrl + "/" + id)
    .pipe(
      catchError(this.handleError<Project>('getProject'))
    );
    
  }

  searchSetOfProjectsByName(name: string, page: number): Observable<any> {
    let params = new HttpParams({
      fromObject: {
        page: page.toString(),
        pageSize: "7"
      }
    });
    return this.http.get<Project[]>(this.projectUrl + "/pagination/" + name, { params })
      .pipe(
        catchError(this.handleError('getProjects', []))
      );
  }

  searchSetOfProjectsByTechnology(technology: string, page: number): Observable<any> {
    let params = new HttpParams({
      fromObject: {
        page: page.toString(),
        pageSize: "7"
      }
    });
    return this.http.get<Project[]>(this.projectUrl + "/pagination/technology/" + technology, { params })
      .pipe(
        catchError(this.handleError('getProjects', []))
      );
  }
}