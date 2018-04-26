import { EmployeeService } from './../services/employee.service';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Floor } from './../common-interfaces/floor';
import { FloorServiceService } from './../services/floor-service.service';
import { Vertex } from './../common-interfaces/vertex';
import { async } from '@angular/core/testing';
import { RoomService } from './../services/room.service';
import { Scale, Doc, PointArray, Rect } from './../../../node_modules/svg.js/svg.js.d';
import { Component, OnInit, Input, SimpleChange, OnChanges } from '@angular/core';
import { Room } from '../common-interfaces/room';
import { colors, styling } from './svgcolors';
declare const SVG: any;
@Component({
  selector: 'app-displayed-map',
  templateUrl: './displayed-map.component.html',
  styleUrls: ['./displayed-map.component.css'],
})
export class DisplayedMapComponent implements OnInit, OnChanges {
  rooms: Room[];
  floor: Floor;
  seats = new Array();
  selectedEmployeeRoomId: number = -1;
  selectedEmployeeSeatId: number = -1;
  selectedEmployeeId: number = -1;
  @Input() floorToDisplay: number;
  drawnMap;
  changeLog: string[] = [];
  constructor(private roomService: RoomService, private floorService: FloorServiceService,    private route: ActivatedRoute,
    private router: Router, private employeeService: EmployeeService) {
   }

  ngOnInit() {
    this.drawnMap = SVG.adopt(document.getElementById('svg'));
    this.drawnMap.circle(100).move(350, 350);
    this.route.params.subscribe( params => this.selectedEmployeeId =  + params['id']);
    //this.getFloor();
    this.route.paramMap
    .switchMap((params: ParamMap) =>
      this.employeeService.getEmployeeLocationInfo(+params.get('id')))
        .subscribe(EmployeeLocationInfo => {
          console.log(EmployeeLocationInfo);
          this.floorToDisplay = EmployeeLocationInfo.FloorId;
          this.selectedEmployeeRoomId = EmployeeLocationInfo.RoomId;
          this.selectedEmployeeSeatId = EmployeeLocationInfo.SeatId;
          //this.selectedEmployeeId = +params.get('id');
          this.getFloor();
          //this.selectedEmployeeRoomId = Employee;
        });  
  }

  getFloor(): void {
    this.floorService.getFloor(this.floorToDisplay)
      .subscribe(
        Floor => {
          this.floor = Floor;
          this.displayMap();
        });
  }

  getFloorByEmployeeId(employeeId: number){
    this.floorService.getFloorByEmployeeId(employeeId)
      .subscribe(
        Floor => {
          this.floor = Floor;
          this.displayMap();
        });
  }
  

  ngOnChanges(changes: {[propKey: string]: SimpleChange}) {
    for (let propName in changes) {
      let changedProp = changes[propName];
      this.floorToDisplay = changedProp.currentValue;
      this.getFloor();
    }
  }

  displayMap() {
    this.drawnMap.clear();
    this.floor.Rooms.forEach(room => {
      //concatenate room vertices into polygon coordinates
      var arra = '';
      if(room.Walls.length != 0) {
        room.Walls.forEach(wall => 
          {
            arra = arra.concat(wall.StartVertex.X + ',' + wall.StartVertex.Y + ' ')
        });
      }
      //drawing room with mouseover and mouseout events
      this.drawnMap
      .polygon(arra).fill(this.selectedEmployeeRoomId === room.Id ? colors.roomWithMouseOverColor : colors.roomColor)
      /*.mouseover(function () {
        this.fill(colors.roomWithMouseOverColor);
        console.log(room.Id + "selected");
      })
      .mouseout(function () {
          this.fill(this.selectedEmployeeRoomId === room.Id ? colors.roomWithMouseOverColor : colors.roomColor)
      })*/
      .stroke({ width: styling.roomBorder, color: colors.backgroundColor })
    });

    this.floor.Walls.forEach(wall => {
      //drawing lines

      this.drawnMap
      .line(wall.StartVertex.X + ", " + wall.StartVertex.Y + ", " + wall.EndVertex.X + ", " + wall.EndVertex.Y)
      .stroke({ width: 2 });
    });
    //drawing seats - needs to be on seperate loop so seats will be alwyays on top
   /* constructor(public router: Router) { }

  ngOnInit() {
  }

  showMore(){
    this.router.navigate(['/main',{outlets: {right: [this.employee.Id], center: [this.employee.Id]} }]);
  }*/
  var self = this;
    this.floor.Rooms.forEach(room => {
      room.Seats.forEach(seat => {
        console.log("Seat: " + seat.Id  + " Dev: "  + seat.DeveloperId);
        this.drawnMap
        .rect(10, 10)
        .move(seat.Vertex.X, seat.Vertex.Y)
        .fill(this.selectedEmployeeRoomId === seat.Id ?  '#fff' : colors.seatColor)
        .stroke({ width: 0 })
        .click(function() {if(!(seat.DeveloperId===null)) self.router.navigate(['/main',{outlets: {right: [seat.DeveloperId], center: [seat.DeveloperId]}}])})
        .draggable(function(x, y){
          //function to move seats snapped to grid
          return {
              x: x - x % 10,
              y: y - y % 10
          };
      })
      .click(function() {
        //will be changed to displaying Employee information TODO
      });
      });
    });
  }
}
