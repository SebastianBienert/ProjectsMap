import { Vertex } from './../common-interfaces/vertex';
import { async } from '@angular/core/testing';
import { RoomService } from './../services/room.service';
import { Scale, Doc, PointArray, Rect } from './../../../node_modules/svg.js/svg.js.d';
import { Component, OnInit } from '@angular/core';
import { Room } from '../common-interfaces/room';
declare const SVG:any;


@Component({
  selector: 'app-drawing-board',
  templateUrl: './drawing-board.component.html',
  styleUrls: ['./drawing-board.component.css'],
 })
export class DrawingBoardComponent implements OnInit {
  rooms : Room[];
  drawMode: string;
  draw;
  
  drawIt($event)
  {
  this.rooms.forEach(room => {
      var arra = '';
      room.Vertexes.forEach(vertex => {
        arra =  arra.concat(vertex.X + ',' +  vertex.Y + ' ')       
      });
      this.draw.polygon(arra).fill('none').stroke({ width: 1 });
    });
  }
  constructor(private roomService: RoomService) { }

  ngOnInit() {
    this.displayData();
  }
  displayData(): void {
    this.draw = SVG('canvas').size(800, 800);
    this.getRooms();
    this.getRooms();
  }

  getRooms(): void {
    this.roomService.getRooms()
      .subscribe(Rooms => this.rooms = Rooms);
  }
}
