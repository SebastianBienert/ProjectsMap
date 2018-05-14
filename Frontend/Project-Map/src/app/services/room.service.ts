import { HandleError, HttpErrorHandler } from './http-error-handler.service';
import { Room } from './../common-interfaces/room';
import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';
import { Globals } from './../globals';

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
  private handleError: HandleError;
  roomUrl: string;
  postRoomUrl = 'http://localhost:58923/api/room';// !!! ???

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler,
    private globals: Globals) {
    this.handleError = httpErrorHandler.createHandleError('RoomService');
    this.roomUrl = globals.getUrl() + '/api/room';
  }

  /** GET rooms from the server */
  getRooms (): Observable<Room[]> {
    return this.http.get<Room[]>(this.roomUrl)
      .pipe(
        catchError(this.handleError('getRooms', []))
      );
  }

  public addRoom(Room) {
    return this.http.post<Room>(this.postRoomUrl, {
      'Walls' : Room.Walls
    })
  }
}