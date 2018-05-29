import { ResponseContentType } from '@angular/http';
import { catchError } from 'rxjs/operators';

import { Floor } from './../common-interfaces/floor';
import { Observable } from 'rxjs/Observable';
import { HttpErrorHandler, HandleError } from './http-error-handler.service';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Globals } from './../globals';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'my-auth-token'
  })
};

@Injectable()
export class FloorServiceService {
  private handleError: HandleError;
  floorUrl: string;
  companyUrl: string;
  buildingUrl: string;
  postFloorUrl: string;
  floorsListUrl: string;
  employeeUrl: string;
  seatUrl: string;
  floorPhotoUrl: string;
  //roomUrl = 'https://projectsmapwebapi.azurewebsites.net/api/room';  // For localhosted webapi
  
  constructor(
    private http: HttpClient,
    httpErrorHandler: HttpErrorHandler,
    private globals: Globals) {
    this.handleError = httpErrorHandler.createHandleError('FloorService');
    this.floorUrl = globals.getUrl() + '/api/floor';
    this.floorPhotoUrl = globals.getUrl() + '/api/floor/photo';
    this.companyUrl = globals.getUrl() + '/api/company/1';
    this.buildingUrl = globals.getUrl() + '/api/buildings';
    this.postFloorUrl = globals.getUrl() + '/api/floor';
    this.floorsListUrl = globals.getUrl() + '/api/floor/list';
    this.employeeUrl = globals.getUrl() + '/api/developers';
    this.seatUrl = globals.getUrl() + '/api/seat';
  }

  getFloor (id: number): Observable<Floor> {
    //,console.log("MeinID"  +  id);
    console.log(this.floorUrl)
    return this.http.get<Floor>(this.floorUrl + "/"  + id)
      .pipe(
        catchError(this.handleError<Floor>('getFloor'))
      );
  }

  // getFloorPhoto (id: number): Observable<any> {
  //   return this.http.get<any>(this.floorPhotoUrl + "/"  + 1)
  //     .pipe(
  //       catchError(this.handleError<any>('getFloorPhoto'))
  //     );
  // }
  getFloorPhoto(id: number): Observable<Blob> {
    return this.http.get(this.floorPhotoUrl+ "/" +id, {responseType: "blob"});
}
  
  getFloorByEmployeeId (id: number): Observable<Floor> {
    return this.http.get<Floor>(this.employeeUrl + "/"  + 3 + "/locationInfo")
      .pipe(
        catchError(this.handleError<Floor>('getFloorByEmployeeId'))
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
    return this.http.post<number>(this.postFloorUrl, {
      'Walls' : Floor.Walls,
      'Rooms' : Floor.Rooms,
      'Description' : Floor.Description,
      'BuildingId' : Floor.BuildingId,
      'FloorNumber' : Floor.FloorNumber,
      'XPhoto' : Floor.XPhoto,
      'YPhoto' : Floor.YPhoto,
      

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
    return this.http.get<any[]>(this.buildingUrl)
      .pipe(
        catchError(this.handleError('getBuildingsList', []))
      );
    }

    public updateFloor(Floor) {
      return this.http.put<Floor>(this.floorUrl + "/" + Floor.Id, {
        'Walls' : Floor.Walls,
        'Rooms' : Floor.Rooms,
        'Description' : Floor.Description,
        'FloorNumber' : Floor.FloorNumber,
        'Id' : Floor.Id
      })
    }

    public assignNewSeat(SeatId: number, UserId: string) {
      console.log("am tryin");
      return this.http.post<any>(this.seatUrl + "/" + SeatId + "/assignEmployee/" + UserId, {
        'seatId' : SeatId,
        'employeeId' : UserId,
      })
    }

    public uploadBackgroundPhoto(fileToUpload: File, id: number) {
      let input = new FormData();
      input.append('file', fileToUpload, fileToUpload.name);
      const url = this.floorUrl + "/photo/" + id;
      console.log(url + "co jest");
      this.http.post(url, input).subscribe(
        res => {
          console.log(res);
        },
        err => {
          console.log("Error during uploading photo occured...");
        }
      );
    }

    
}
