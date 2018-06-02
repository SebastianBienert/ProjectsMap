import { Router } from '@angular/router';
import { DisplayedMapComponent } from './../displayed-map/displayed-map.component';
import { FloorServiceService } from './../services/floor-service.service';
import { Component, OnInit, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-map-navigator',
  templateUrl: './map-navigator.component.html',
  styleUrls: ['./map-navigator.component.css']
})
export class MapNavigatorComponent implements OnInit {
  @ViewChild('content') modalZiom;
  buildingAddress: string;
  displayMode = "loading";
  hell: boolean = true;
  buildingsList = Array();
  modalReference : any;
  currentBuildingFloorsList = Array();
  selectedFloor: number;//change to read id of first floor in floors list and secure from null
  selectedBuilding: number;//change to read id of first building in buildings list and secure from null
  constructor(private floorService: FloorServiceService, private router: Router, private modalService: NgbModal) { }
  
  ngAfterViewInit(){
    setTimeout(() => {
      this.open(this.modalZiom);
    })
    
  }
  ngOnInit() {
    
   // this.modalReference.close();
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

    this.modalReference.close();
  }

  changeBuilding(buildingId: number, floorId: number) {
    //this.router.navigate(['/main']);
    this.currentBuildingFloorsList.length = 0;
    this.getFloorsList(buildingId, floorId);
    this.selectedBuilding = buildingId;
  }
  debug() {
  }

  getFloorsList(BuildingId: number, floorId: number): void {
    this.floorService.getBuildingFloorsList(BuildingId)
      .subscribe(
        FloorsList => {
          this.currentBuildingFloorsList = FloorsList.sort(function (a, b) {
            return a.FloorNumber - b.FloorNumber;
          });

          if(floorId === 0) {
            this.changeFloor(this.currentBuildingFloorsList[0].Id);
            this.modalReference.close();
          }
          else {
            this.changeFloor(floorId);
            this.modalReference.close();
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
                  this.modalReference.close();
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
      this.getFloorsList(this.selectedBuilding, 0);
    }
  }

  mapChanged(mapChanged) {
    // if(mapChanged === 0)
    //   this.modalReference.close();
    //!!! needs to be implemented this.changeBuilding();
   // this.selectedFloor = mapChanged.floorId;
   console.log(mapChanged.buildingId + "  sas   " +  mapChanged.floorId)
    this.changeBuilding(mapChanged.buildingId, mapChanged.floorId)
    //this.selectedBuilding = mapChanged.buildingId;
    //this.changeFloor(mapChanged.floorId);
  }

  editMap() {
    this.displayMode = 'edit';
  }

  open(content) {
    this.modalReference = this.modalService.open(content)
    this.modalReference.result.then((result) => {
      //this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
     // this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
}
