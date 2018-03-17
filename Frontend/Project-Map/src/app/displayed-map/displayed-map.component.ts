import { Floor } from './../common-interfaces/floor';
import { FloorServiceService } from './../services/floor-service.service';
import { Vertex } from './../common-interfaces/vertex';
import { async } from '@angular/core/testing';
import { RoomService } from './../services/room.service';
import { Scale, Doc, PointArray, Rect } from './../../../node_modules/svg.js/svg.js.d';
import { Component, OnInit, Input, SimpleChange, OnChanges } from '@angular/core';
import { Room } from '../common-interfaces/room';
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
  @Input() floorToDisplay: number;
  drawnMap;
  changeLog: string[] = [];
  constructor(private roomService: RoomService, private floorService: FloorServiceService) {
   }

  ngOnInit() {
    this.getFloor();
  }

  getFloor(): void {
    this.floorService.getFloor(this.floorToDisplay)
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
    this.drawnMap = SVG.adopt(document.getElementById('svg')).panZoom({zoomMin: 0.5, zoomMax: 2});
    this.drawnMap.clear();
    this.floor.Rooms.forEach(room => {
      //concatenate room vertices into polygon coordinates
      var arra = '';
      room.Walls.forEach(wall => 
        {
          arra = arra.concat(wall.StartVertex.X + ',' + wall.StartVertex.Y + ' ')
      });
      //drawing room with mouseover and mouseout events
      this.drawnMap
      .polygon(arra).fill('#999')
      .mouseover(function () {
        this.fill({ color: '#AAA' })
      .mouseout(function () {
          this.fill({ color: '#999' })
        })
      })
      .stroke({ width: 2 })
    });

    this.floor.Walls.forEach(wall => {
      //drawing lines

      this.drawnMap
      .line(wall.StartVertex.X + ", " + wall.StartVertex.Y + ", " + wall.EndVertex.X + ", " + wall.EndVertex.Y)
      .stroke({ width: 2 });
    });
    //drawing seats - needs to be on seperate loop so seats will be alwyays on top
    this.floor.Rooms.forEach(room => {
      room.Seats.forEach(seat => {
        this.drawnMap
        .rect(10, 10)
        .move(seat.X, seat.Y)
        .fill('#123')
        .stroke({ width: 0 })
        .draggable(function(x, y){
          //function to move seats snapped to grid
          return {
              x: x - x % 10,
              y: y - y % 10
          };
      })
      .click(function() {
        //will be changed to displaying Employee information TODO
        console.log(seat.Id);
      });
      });
    });
    
  }
}
