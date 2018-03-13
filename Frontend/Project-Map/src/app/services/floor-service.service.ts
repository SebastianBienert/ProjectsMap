import { catchError } from 'rxjs/operators';

import { Floor } from './../common-interfaces/floor';
import { Observable } from 'rxjs/Observable';
import { HttpErrorHandler, HandleError } from './http-error-handler.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class FloorServiceService {

  floorUrl = 'http://localhost:58923/api/floor';  // For localhosted webapi
  companyUrl = 'http://localhost:58923/api/company/1';
  //roomUrl = 'https://projectsmapwebapi.azurewebsites.net/api/room';  // For localhosted webapi
  private handleError: HandleError;
  buildingUrl = 'http://localhost:58923/api/buildings';
  postFloorUrl = 'http://localhost:58923/api/floor';// !!! ???
  floorsListUrl = 'http://localhost:58923/api/floor/list';

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('FloorService');
  }

  /** GET rooms from the server */
 /* getFloors (): Observable<Floor[]> {
    return this.http.get<Floor[]>(this.floorUrl)
      .pipe(
        catchError(this.handleError('getFloors', []))
      );
  }*/
  getFloor (id: number): Observable<Floor> {
    return this.http.get<Floor>(this.floorUrl + "/"  + id)
      .pipe(
        catchError(this.handleError<Floor>('getFloor'))
      );
  }
  public addFloor(Floor) {
    return this.http.post<Floor>(this.postFloorUrl, {
      'Walls' : Floor.Walls,
      'Rooms' : Floor.Rooms,
      'Description' : Floor.Description,
      'BuildingId' : Floor.BuildingId
    })
  }

  getBuildingFloorsList (buildingId : number): Observable<string[]> {
    return this.http.get<string[]>(this.buildingUrl +"/" +buildingId + "/floors")
      .pipe(
        catchError(this.handleError('getBuildingFloorsList', []))
      );
  }

  getBuildingsList (): Observable<string[]> {
    return this.http.get<string[]>(this.companyUrl +"/buildings")
      .pipe(
        catchError(this.handleError('getBuildingsList', []))
      );
    }
}
