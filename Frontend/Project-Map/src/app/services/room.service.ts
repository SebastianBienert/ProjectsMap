import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { Room } from './../common-interfaces/room';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';


import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { catchError } from 'rxjs/operators';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type':  'application/json',
    'Authorization': 'my-auth-token'
  })
};

@Injectable()
export class RoomService {
  roomUrl = 'http://localhost:58923/api/room';  // For localhosted webapi
  //roomUrl = 'https://projectsmapwebapi.azurewebsites.net/api/room';  // For localhosted webapi
  private handleError: HandleError;

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('RoomService');
  }

  /** GET rooms from the server */
  getRooms (): Observable<Room[]> {
    return this.http.get<Room[]>(this.roomUrl)
      .pipe(
        catchError(this.handleError('getRooms', []))
      );
  }
}