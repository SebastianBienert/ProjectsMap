import { DisplayedMapComponent } from './../displayed-map/displayed-map.component';
import { FloorServiceService } from './../services/floor-service.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-map-navigator',
  templateUrl: './map-navigator.component.html',
  styleUrls: ['./map-navigator.component.css']
})
export class MapNavigatorComponent implements OnInit {
  createFloorButtons(){
    throw new Error("Method not implemented.");
  }
  buildingsList = Array();
  currentBuildingFloorsList = Array();
  selectedFloor : number = 1;
  constructor(private floorService: FloorServiceService) { }

  ngOnInit() {
    this.getFloorList();
  }

changeFloor(Id: number)
{
  this.selectedFloor=Id;
  this.getFloorList();
}
getFloorList(): void {
  this.floorService.getFloorList()
    .subscribe(
      FloorList => {
        this.currentBuildingFloorsList = FloorList;
      });
}

}
