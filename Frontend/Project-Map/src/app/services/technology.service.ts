import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { Employee } from './../common-interfaces/employee';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Globals } from './../globals';

import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError } from 'rxjs/operators';
import { Subject } from 'rxjs/Subject';
import { Technology } from '../common-interfaces/technology';



const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'my-auth-token'
  })
};

@Injectable()
export class TechnologyService {
   technologyUrl: string;
   private handleError: HandleError;
   searchedTechnologies : Observable<Technology[]> = new Observable<Technology[]>();

   constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler,
    private globals: Globals) {
      this.handleError = httpErrorHandler.createHandleError('TechnologyService');
      this.searchedTechnologies = this.getTechnologies();
      this.technologyUrl = globals.getUrl() + '/api/technology';
    }

     getTechnologies(): Observable<Technology[]> {
      return this.http.get<Technology[]>(this.technologyUrl)
        .pipe(
          catchError(this.handleError('getTechnologies', []))
        );
    }

    getTechnologiesNames(): Observable<string[]>{
      return this.http.get<Technology[]>(this.technologyUrl)
      .map((technologies : Technology[]) => technologies.map(t => t.Name));
      
    }
}
