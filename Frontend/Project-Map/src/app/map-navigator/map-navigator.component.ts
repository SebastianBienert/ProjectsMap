import { DisplayedMapComponent } from './../displayed-map/displayed-map.component';
import { FloorServiceService } from './../services/floor-service.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-map-navigator',
  templateUrl: './map-navigator.component.html',
  styleUrls: ['./map-navigator.component.css']
})
export class MapNavigatorComponent implements OnInit {
  buildingAddress : string;
  displayMode = "display";
  hell : boolean = true;
  buildingsList = Array();
  currentBuildingFloorsList = Array();
  selectedFloor : number = 1;//change to read id of first floor in floors list and secure from null
  selectedBuilding : number = 1;//change to read id of first building in buildings list and secure from null
  constructor(private floorService: FloorServiceService) { }

  ngOnInit() {
    this.getBuildingsList();
    this.getFloorsList(1);
  }

  addNewFloor()
  {
    this.displayMode = "create";
    this.selectedFloor = 0;
  }

  addNewBuilding()
  {
    this.selectedBuilding = 0;
    this.currentBuildingFloorsList.length =0;
    this.displayMode = "addBuilding";
  }
  saveNewBuilding() {
    var building =  {Address: this.buildingAddress, CompanyId: 1}//companyId==1 for test purposes
    this.floorService.addBuilding(building).subscribe(fun => {this.getBuildingsList()});
  }
changeFloor(Id: number)
{
  this.displayMode = "display";
  this.selectedFloor=Id;
}

changeBuilding(Id: number)
{
  this.displayMode = "display";
  this.selectedBuilding = Id;
  this.getFloorsList(Id);
  this.selectedFloor = this.buildingsList.find(x => x.Id === Id).FloorsIds[0];
}
debug()
{
  console.log(this.buildingsList[1]);
}

getFloorsList(BuildingId : number): void {
  this.floorService.getBuildingFloorsList(BuildingId)
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
mapCreated(mapCreated: boolean) {
  if (mapCreated) {
    this.getFloorsList(this.selectedBuilding);
  }
}

}
