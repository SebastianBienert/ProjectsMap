import { DisplayedMapComponent } from './../displayed-map/displayed-map.component';
import { FloorServiceService } from './../services/floor-service.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-map-navigator',
  templateUrl: './map-navigator.component.html',
  styleUrls: ['./map-navigator.component.css']
})
export class MapNavigatorComponent implements OnInit {
  buildingAddress: string;
  displayMode = "loading";
  hell: boolean = true;
  buildingsList = Array();
  currentBuildingFloorsList = Array();
  selectedFloor: number;//change to read id of first floor in floors list and secure from null
  selectedBuilding: number;//change to read id of first building in buildings list and secure from null
  constructor(private floorService: FloorServiceService) { }

  ngOnInit() {
    this.getBuildingsList();
  }

  addNewFloor() {
    this.displayMode = "create";
    this.selectedFloor = 0;
  }

  addNewBuilding() {
    this.selectedBuilding = 0;
    this.currentBuildingFloorsList.length = 0;
    this.displayMode = "addBuilding";
  }
  saveNewBuilding() {
    var building = { Address: this.buildingAddress, CompanyId: 1 }//companyId==1 for test purposes
    this.floorService.addBuilding(building).subscribe(fun => { this.getBuildingsList() });
  }

  changeFloor(Id: number) {
    this.displayMode = "displayMap";
    this.selectedFloor = Id;
  }

  changeBuilding(Id: number) {
    this.selectedBuilding = Id;
    this.selectedFloor = this.buildingsList.find(x => x.Id === Id).FloorsIds[0];
    this.displayMode = "loading";
    this.getFloorsList(Id);
  }
  debug() {
    console.log(this.buildingsList[1]);
  }

  getFloorsList(BuildingId: number): void {
    this.floorService.getBuildingFloorsList(BuildingId)
      .subscribe(
        FloorsList => {
          this.currentBuildingFloorsList = FloorsList.sort(function(a, b) {
            return a.FloorNumber - b.FloorNumber;
          });;
          if (FloorsList.length > 0) {
            this.selectedFloor = FloorsList[0].Id;
            this.displayMode = 'displayMap';
          }
        });
  }

  getBuildingsList(): void {
    this.floorService.getBuildingsList()
      .subscribe(
        BuildingsList => {
          this.buildingsList = BuildingsList;
          if (this.buildingsList.length > 0 && this.buildingsList[0].FloorsIds.length > 0) {
            this.getFloorsList(BuildingsList[0].FloorsIds[0]);
            this.selectedBuilding = BuildingsList[0].Id;
            console.log(this.selectedBuilding);
          }
        });
  }

  mapCreated(mapCreated: boolean) {
    if (mapCreated) {
      this.getFloorsList(this.selectedBuilding);
    }
  }

  editMap() {
    this.displayMode = 'edit';
  }

}
