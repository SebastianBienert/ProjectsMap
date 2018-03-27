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
    // var json = {
    //   'Walls' : Floor.Walls,
    //   'Rooms' : Floor.Rooms,
    //   'Description' : Floor.Description,
    //   'BuildingId' : Floor.BuildingId,
    //   'FloorNumber' : Floor.FloorNumber
    // };
    
    // this.toTestData(json);
    return this.http.post<Floor>(this.postFloorUrl, {
      'Walls' : Floor.Walls,
      'Rooms' : Floor.Rooms,
      'Description' : Floor.Description,
      'BuildingId' : Floor.BuildingId,
      'FloorNumber' : Floor.FloorNumber
    })
  }
  toTestData(json) {
    var arra = 'new Floor()\n{\nBuildingId=1,\nDescription = "heh",\nFloorNumber = ' + json.FloorNumber + ",\n";
    arra = arra.concat("Rooms = new List<Room>()\n{\n")
    json.Rooms.forEach(Room => {
      arra =arra.concat("new Room()\n{\nSeats = new List<Seat>\n{\n")
      Room.Seats.forEach(element => {
        arra = arra.concat("new Seat()\n{\nEmployee = null,\nX = " + element.X +",\nY = " + element.Y+"\n},\n");
      });
      arra = arra.concat("\n},\n")
      arra = arra.concat("Walls = new List<Wall>\n{\n");
      Room.Walls.forEach(Wall => {
        arra = arra.concat("new Wall\n{\n");
        arra = arra.concat("StartVertexX = " + Wall.StartVertex.X + ",\n")
        arra = arra.concat("StartVertexY = " + Wall.StartVertex.Y + ",\n")
        arra = arra.concat("EndVertexX = " + Wall.EndVertex.X + ",\n")
        arra = arra.concat("EndVertexY = " + Wall.EndVertex.Y + ",\n")
        arra = arra.concat("},\n")
      });
      arra = arra.concat("\n}\n},")
    });
    arra = arra.concat("}\n}\n},\nWalls = new List<Wall>\n{\n")
    json.Walls.forEach(Wall => {
      arra = arra.concat("\nnew Wall\n{\n");
      arra = arra.concat("StartVertexX = " + Wall.StartVertex.X + ",\n")
      arra = arra.concat("StartVertexY = " + Wall.StartVertex.Y + ",\n")
      arra = arra.concat("EndVertexX = " + Wall.EndVertex.X + ",\n")
      arra = arra.concat("EndVertexY = " + Wall.EndVertex.Y + ",\n")
      arra = arra.concat("},\n")
    });
    arra = arra.concat("\n}\n},")
    console.log(arra);
  }

  public addBuilding(Building) {
    return this.http.post<any>(this.buildingUrl, {
      'Address' : Building.Address,
      'CompanyId' : Building.CompanyId,
    })
  }

  getBuildingFloorsList (buildingId : number): Observable<Floor[]> {
    return this.http.get<Floor[]>(this.buildingUrl +"/" +buildingId + "/floors")
      .pipe(
        catchError(this.handleError('getBuildingFloorsList', []))
      );
  }

  getBuildingsList (): Observable<any[]> {
    return this.http.get<any[]>(this.companyUrl +"/buildings")
      .pipe(
        catchError(this.handleError('getBuildingsList', []))
      );
    }

    public updateFloor(Floor) {
      // var json = {
      //   'Walls' : Floor.Walls,
      //   'Rooms' : Floor.Rooms,
      //   'Description' : Floor.Description,
      //   'BuildingId' : Floor.BuildingId,
      //   'FloorNumber' : Floor.FloorNumber
      // };
      
      //this.toTestData(json);
      console.log("updating");
      console.log(Floor);
      return this.http.put<Floor>(this.floorUrl + "/" + Floor.Id, {
        'Walls' : Floor.Walls,
        'Rooms' : Floor.Rooms,
        'Description' : Floor.Description,
        'FloorNumber' : Floor.FloorNumber,
        'Id' : Floor.Id
      })
    }
}
