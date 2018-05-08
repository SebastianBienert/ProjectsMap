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
    this.selectedFloor = Id;
    this.displayMode = "displayMap";
  }

  changeBuilding(Id: number) {
    this.displayMode = "loading";
    this.getFloorsList(Id, true);
    this.selectedBuilding = Id;
  }
  debug() {
    console.log(this.buildingsList[1]);
  }

  getFloorsList(BuildingId: number, loadFirstFloor: boolean): void {
    this.floorService.getBuildingFloorsList(BuildingId)
      .subscribe(
        FloorsList => {
          this.currentBuildingFloorsList = FloorsList.sort(function (a, b) {
            return a.FloorNumber - b.FloorNumber;
          });

          if(loadFirstFloor) {
            this.changeFloor(this.currentBuildingFloorsList[0].Id);
          }
        });
  }

  displayFloor(floorId: number) {
    this.selectedFloor = floorId;
    this.displayMode = 'displayMap';
  }

  getBuildingsList(): void {
    this.floorService.getBuildingsList()
      .subscribe(
        BuildingsList => {
          this.buildingsList = BuildingsList;
          if (this.buildingsList.length > 0 && this.buildingsList[0].FloorsIds.length > 0) {
            this.selectedBuilding = this.buildingsList[0].Id;
            this.floorService.getBuildingFloorsList(this.buildingsList[0].Id)
              .subscribe(
                FloorsList => {
                  this.currentBuildingFloorsList = FloorsList.sort(function (a, b) {
                    return a.FloorNumber - b.FloorNumber;
                  });
                  this.displayFloor(this.currentBuildingFloorsList[0].Id);
                }
              );
            // this.getFloorsList(BuildingsList[0].FloorsIds[0]);
            // if (this.currentBuildingFloorsList.length > 0) {
            //   this.displayFloor(this.currentBuildingFloorsList[0].Id);
            // }
            // this.selectedBuilding = BuildingsList[0].Id;
          }
        });
  }

  mapCreated(mapCreated: boolean) {
    if (mapCreated) {
      this.getFloorsList(this.selectedBuilding, false);
    }
  }

  mapChanged(mapChanged: number) {
    console.log("Changed");
    //!!! needs to be implemented this.changeBuilding();
    this.changeFloor(mapChanged);
  }

  editMap() {
    this.displayMode = 'edit';
  }

}
