import { DisplayedMapComponent } from './../displayed-map/displayed-map.component';
import { FloorServiceService } from './../services/floor-service.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-map-navigator',
  templateUrl: './map-navigator.component.html',
  styleUrls: ['./map-navigator.component.css']
})
export class MapNavigatorComponent implements OnInit {
  
  buildingsList = Array();
  currentBuildingFloorsList = Array();
  selectedFloor : number = 1;//change to read id of first floor in floors list and secure from null
  selectedBuilding : number = 1;//change to read id of first building in buildings list and secure from null
  constructor(private floorService: FloorServiceService) { }

  ngOnInit() {
    this.getBuildingsList();
    this.getFloorsList(1);
  }

changeFloor(Id: number)
{
  this.selectedFloor=Id;
  //this.getFloorsList(Id);///get outta hija
}

changeBuilding(Id: number)
{
  this.selectedBuilding = Id;
  this.getFloorsList(Id);
  //console.log(this.selectedFloor = this.buildingsList.find(x => x.Id === Id).FloorsIds[0]);
  this.selectedFloor = this.buildingsList.find(x => x.Id === Id).FloorsIds[0];
}
debug()
{
  console.log(this.buildingsList[1]);
}

getFloorsList(BuildingId : number): void {
  this.floorService.getBuildingFloorsList(BuildingId)//'1' for test purpose only !!!
    .subscribe(
      FloorsList => {
        this.currentBuildingFloorsList = FloorsList;
      });
}

getBuildingsList(): void {
  this.floorService.getBuildingsList()
    .subscribe(
      BuildingsList => {
        this.buildingsList = BuildingsList;
      });
}

}
