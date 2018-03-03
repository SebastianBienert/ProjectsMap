import { Vertex } from './../common-interfaces/vertex';
import { async } from '@angular/core/testing';
import { RoomService } from './../services/room.service';
import { Scale, Doc, PointArray, Rect, Line } from './../../../node_modules/svg.js/svg.js.d';
import { Component, OnInit } from '@angular/core';
import { Room } from '../common-interfaces/room';
declare const SVG: any;

@Component({
  selector: 'app-map-creator',
  templateUrl: './map-creator.component.html',
  styleUrls: ['./map-creator.component.css']
})
export class MapCreatorComponent implements OnInit {
  drawnMap;
  rooms: Room[];
  drawMode: string;
  drawingRightNow: boolean = false;
  createdRooms = Array();
  rects = Array();
  lines = Array();
  constructor(private roomService: RoomService) {

  }
  setDrawingRightNow(bool: boolean) {
    this.drawingRightNow = bool;
  }
  ngOnInit() {
    this.displayMap();
  }

  drawNewRoom() {
    this.drawMode = 'room';
    this.drawnMap.off("mousedown");
    this.drawnMap.off("mouseup");
    if (!this.drawingRightNow) {
      var poly = this.drawnMap.polygon().draw({ snapToGrid: 10 }).fill('#00f').stroke({ width: 1 });
      this.createdRooms.push(poly);
      this.drawingRightNow = true;
      var self = this;
      poly.on('drawstart', function (e) {
        document.addEventListener('keydown', function (e) {
          if (e.keyCode == 13) {
            poly.draw('done');
            poly.off('drawstart');
            self.drawingRightNow = false;
          }
        });
        document.addEventListener('keydown', function (e) {
          if (e.keyCode == 27) {
            poly.draw('cancel');
            poly.off('drawstart');
            self.drawingRightNow = false;
          }
        });
      });

    }
  }

  deleteLastRoom() {
    if (this.createdRooms.length > 0) {
      this.createdRooms[this.createdRooms.length - 1].remove();
      this.createdRooms.pop();
    }
  }

  displayMap() {
    this.drawnMap = SVG('canvas').size(800, 800);
    this.drawnMap.rect(800, 800).fill('none').stroke({ width: 2 });
  }

  chooseLine() {
    var self = this;
    this.drawMode = 'line';
    this.drawnMap.off("mousedown");
    this.drawnMap.off("mouseup");
    var line = this.drawnMap.line().fill('none').stroke({ width: 3 });
    this.drawnMap.on(
      "mousedown",
      function (e) {
        line.draw(e, { snapToGrid: 20 });
      },
      false
    );

    this.drawnMap.on(
      "mouseup",
      function (e) {
        line.draw("stop", e);
        self.lines.push(line);
      },
      false
    );
  }

  chooseSquare() {
    var self = this;
    this.drawnMap.off("mousedown");
    this.drawnMap.off("mouseup");
    this.drawMode = 'square';
    var rect = this.drawnMap.rect().fill('none').stroke({ width: 3 });
    this.drawnMap.on(
      "mousedown",
      function (e) {
        rect.draw(e, { snapToGrid: 20 });
      },
      false
    );

    this.drawnMap.on(
      "mouseup",
      function (e) {
        rect.draw("stop", e);
        self.rects.push(rect);
      },
      false
    );
  }


  determineShape() {
    switch (this.drawMode) {
      case 'square':
        this.chooseSquare();
        break;
      case 'line':
        this.chooseLine();
        break;
    }
  }

  saveMap() {

  }
  
}
