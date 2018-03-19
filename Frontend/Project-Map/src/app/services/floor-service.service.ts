import { catchError } from 'rxjs/operators';

import { Floor } from './../common-interfaces/floor';
import { Observable } from 'rxjs/Observable';
import { HttpErrorHandler, HandleError } from './http-error-handler.service';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable()
export class FloorServiceService {

  floorUrl = 'https://projectsmapwebapi.azurewebsites.net/api/floor';  // For localhosted webapi
  companyUrl = 'https://projectsmapwebapi.azurewebsites.net/api/company/1';
  //roomUrl = 'https://projectsmapwebapi.azurewebsites.net/api/room';  // For localhosted webapi
  private handleError: HandleError;
  buildingUrl = 'https://projectsmapwebapi.azurewebsites.net/api/buildings';
  postFloorUrl = 'https://projectsmapwebapi.azurewebsites.net/api/floor';
  floorsListUrl = 'https://projectsmapwebapi.azurewebsites.net/api/floor/list';

  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) {
    this.handleError = httpErrorHandler.createHandleError('FloorService');
  }

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
      'BuildingId' : Floor.BuildingId,
      'FloorNumber' : Floor.FloorNumber
    })
  }

  public addBuilding(Building) {
    return this.http.post<any>(this.buildingUrl, {
      'Address' : Building.Address,
      'CompanyId' : Building.CompanyId,
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
