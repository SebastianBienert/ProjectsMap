import { FloorServiceService } from './../services/floor-service.service';
import { Floor } from './../common-interfaces/floor';
import { Wall } from './../common-interfaces/wall';
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
  constructor(private roomService: RoomService, private floorService: FloorServiceService) {
  }
  setDrawingRightNow(bool: boolean) {
    this.drawingRightNow = bool;
  }
  ngOnInit() {
    this.displayMap();
  }
  /*
    stopDrawingAtEnter(e, poly) {
      if (e.keyCode == 13) {
        poly.draw('done');
        poly.off('drawstart');
        this.drawingRightNow = false;
        this.createdRooms.push(poly);
        console.log(this.createdRooms.length);
        document.addEventListener('keydown', this.stopDrawingAtEnter(e, poly);
      }
    }
  */
  drawNewRoom() {
    this.drawMode = 'room';
    this.drawnMap.off("mousedown");
    this.drawnMap.off("mouseup");
    if (!this.drawingRightNow) {

      var poly = this.drawnMap.polygon().draw({ snapToGrid: 10 }).fill('#4f4').stroke({ width: 1 });

      this.drawingRightNow = true;
      var self = this;
      poly.on('drawstart', function (e) {
        document.addEventListener('keydown', function stopDrawingAtEnter(e) {
          if (e.keyCode == 13) {
            poly.draw('done');
            poly.off('drawstart');
            self.drawingRightNow = false;
            self.createdRooms.push(poly);
            console.log(self.createdRooms.length);
            document.removeEventListener('keydown', stopDrawingAtEnter);
            //add seats TODO !!!
            /*poly.click(function placeSeat(event) {
              //seat size hardcoded to 10x10 for now !!! alligned to grid: 10
              var seat = self.drawnMap.rect(10,10).move(( event.offsetX - event.offsetX%10), ( event.offsetY - event.offsetY%10));
              //seat.draw();
            })*/
          }
        });
        document.addEventListener('keydown', function cancelDrawingAtEscape(e) {
          if (e.keyCode == 27) {
            poly.draw('cancel');
            poly.off('drawstart');
            self.drawingRightNow = false;
            document.removeEventListener('keydown', cancelDrawingAtEscape);
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
    this.drawnMap =SVG.adopt(document.getElementById('createMap'));
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

  //add allert so that user will know that saving map will end map creation!
  saveMap() {
    var linesVertexes = Array();
    //parse svg.js lines to LineMap
    for (var i = 0; i < this.lines.length; i++) {
      linesVertexes.push(this.coordinatesToLine(this.lines[i].attr('x1'),this.lines[i].attr('y1'),this.lines[i].attr('x2'),this.lines[i].attr('y2')));
    }
   //parse svg.js rects to LineMap
    for (var i = 0; i < this.rects.length; i++) {
      linesVertexes.push(...this.rectToLines(this.rects[i]));
   }
    //polygon to lines
    var Rooms = Array();
    for (var i = 0; i < this.createdRooms.length; i++) {
      var oneCoordinateAtTime = this.createdRooms[i].attr('points').split(/[\s,]+/);
      var Walls = Array();
      for (var j = 0; j < (oneCoordinateAtTime.length-2); j+=2) {
        Walls.push(this.coordinatesToLine(oneCoordinateAtTime[j], oneCoordinateAtTime[j+1], oneCoordinateAtTime[j+2], oneCoordinateAtTime[j+3]));
      }
      Walls.push(this.coordinatesToLine(oneCoordinateAtTime[oneCoordinateAtTime.length-2], oneCoordinateAtTime[oneCoordinateAtTime.length-1], oneCoordinateAtTime[0], oneCoordinateAtTime[1]));
      //push seats and Projects as empty
      var Seats = Array();
      var room: Room = {Walls, Seats} as Room;
      Rooms.push(room);
    }

    var floor: Floor = {Rooms, Walls:linesVertexes, BuildingId:1, Description:""} as Floor;
    
    console.log(floor);
    this.floorService.addFloor(floor).subscribe();

    //clear map
    this.lines.length=0;
    this.rects.length=0;
    this.createdRooms.length=0;
    this.drawnMap.clear();
  }

  rectToLines(rect: Rect): Wall[]{
      var lines = Array();

      lines.push(this.coordinatesToLine(rect.attr('x'), rect.attr('y'), rect.attr('x') + rect.attr('width'), rect.attr('y')));
      lines.push(this.coordinatesToLine(rect.attr('x'), rect.attr('y'), rect.attr('x'), rect.attr('y') + rect.attr('height')));
      lines.push(this.coordinatesToLine(rect.attr('x') + rect.attr('width'), rect.attr('y'), rect.attr('x') + rect.attr('width'), rect.attr('y') + rect.attr('height')));
      lines.push(this.coordinatesToLine(rect.attr('x'), rect.attr('y')+ rect.attr('height'), rect.attr('x') + rect.attr('width'), rect.attr('y') + rect.attr('height')));
      return lines;
  }

  coordinatesToLine(x1, y1, x2, y2) : Wall{
    var X = x1;
    var Y = y1
    var StartVertex = { X, Y } as Vertex;

    X = x2;
    Y = y2;
    var EndVertex = { X, Y } as Vertex;

    var line: Wall = { StartVertex, EndVertex } as Wall;
    return line;
  }
}
