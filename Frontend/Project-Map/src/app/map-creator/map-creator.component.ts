import { Vertex } from './../common-interfaces/vertex';
import { async } from '@angular/core/testing';
import { RoomService } from './../services/room.service';
import { Scale, Doc, PointArray, Rect } from './../../../node_modules/svg.js/svg.js.d';
import { Component, OnInit } from '@angular/core';
import { Room } from '../common-interfaces/room';
//import * as SVG from 'svg.js';// 'svg.draw.js', 'svg.whatever.js'
declare const SVG:any;

@Component({
  selector: 'app-map-creator',
  templateUrl: './map-creator.component.html',
  styleUrls: ['./map-creator.component.css']
})
export class MapCreatorComponent implements OnInit {
  rooms : Room[];
  drawMode: string;
  drawingRightNow: boolean = false;
  createdMap;
 
  constructor(private roomService: RoomService) { }
  setDrawingRightNow(bool: boolean)
  {
    this.drawingRightNow = bool;
  }
  ngOnInit() {
    this.displayMap();
  }

  drawPolygon() {
  }

  displayMap() {
  }
}
