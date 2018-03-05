import { Vertex } from './../common-interfaces/vertex';
import { async } from '@angular/core/testing';
import { RoomService } from './../services/room.service';
import { Scale, Doc, PointArray, Rect } from './../../../node_modules/svg.js/svg.js.d';
import { Component, OnInit } from '@angular/core';
import { Room } from '../common-interfaces/room';
declare const SVG: any;
@Component({
  selector: 'app-displayed-map',
  templateUrl: './displayed-map.component.html',
  styleUrls: ['./displayed-map.component.css'],
})
export class DisplayedMapComponent implements OnInit {
  rooms: Room[];
  seats = new Array();
  drawnMap;

  constructor(private roomService: RoomService) {
   }

  ngOnInit() {
    this.getRooms();
  }

  getRooms(): void {
    this.roomService.getRooms()
      .subscribe(
        Rooms => {
          this.rooms = Rooms;
          this.displayMap()
        });
  }

  displayMap() {
    this.drawnMap = SVG('canvas').size(800, 800).panZoom();

    this.rooms.forEach(room => {
      //concatenate room vertices into polygon coordinates
      var arra = '';


      arra = arra.concat(room.Walls[0].StartVertex.X + ',' + room.Walls[0].StartVertex.Y + ' ');
      arra = arra.concat(room.Walls[0].EndVertex.X + ',' + room.Walls[0].EndVertex.Y + ' ');
      room.Walls.slice(1,room.Walls.length - 1).forEach(wall => {
        arra = arra.concat(wall.EndVertex.X + ',' + wall.EndVertex.Y + ' ');
      })
      
      /*room.Vertexes.forEach(vertex => 
        {
        arra = arra.concat(vertex.X + ',' + vertex.Y + ' ')
      });*/
      //drawing room with mouseover and mouseout events
      this.drawnMap
      .polygon(arra).fill('#fff')
      .mouseover(function () {
        this.fill({ color: '#1f2' })
      .mouseout(function () {
          this.fill({ color: '#fff' })
        })
      })
      .stroke({ width: 2 })
    });
    //drawing seats - needs to be on seperate loop so seats will be alwyays on top
    this.rooms.forEach(room => {
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
