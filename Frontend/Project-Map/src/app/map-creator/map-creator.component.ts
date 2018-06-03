import { defaultValues } from './defaultValues';
import { FloorServiceService } from './../services/floor-service.service';
import { Floor } from './../common-interfaces/floor';
import { Wall } from './../common-interfaces/wall';
import { Vertex } from './../common-interfaces/vertex';
import { async } from '@angular/core/testing';
import { RoomService } from './../services/room.service';
import { Scale, Doc, PointArray, Rect, Line } from './../../../node_modules/svg.js/svg.js.d';
import { Component, OnInit, Input, Output, EventEmitter, ElementRef } from '@angular/core';
import { Room } from '../common-interfaces/room';
import { Seat } from '../common-interfaces/seat';
declare const SVG: any;

@Component({
  selector: 'app-map-creator',
  templateUrl: './map-creator.component.html',
  styleUrls: ['./map-creator.component.css']
})
export class MapCreatorComponent implements OnInit {
  test() {
    //var polygon = this.drawnMap.polygon('331,251 331,248 320,248 310,248 310,244 310,240 298,240 287,240 287,238 287,236 219,236 124,235 97,236 75,237 54,237 54,230 54,224 31,224 24,224 41,224 41,210 41,196 36,195 32,195 37,195 42,195 42,209 42,224 50,224 58,224 58,210 58,196 52,195 47,195 53,195 59,195 59,209 59,224 57,224 55,224 55,230 54,236 75,236 96,235 97,234 98,234 98,213 98,193 119,193 140,193 120,194 100,194 100,214 100,234 125,235 164,235 177,235 177,226 177,218 179,218 181,218 181,206 181,194 163,194 146,193 164,193 182,193 182,206 182,219 180,219 179,219 179,227 179,235 197,235 216,235 216,231 216,227 217,227 217,228 216,231 216,235 227,235 237,235 237,228 237,221 238,228 238,214 238,194 247,193 236,193 227,193 238,194 238,200 238,207 227,207 217,206 227,206 237,206 237,200 237,194 227,194 216,193 238,193 259,192 264,191 268,190 268,190 262,192 248,194 239,194 239,215 239,235 267,235 295,235 295,217 295,199 296,190 296,182 288,182 280,182 280,184 280,186 277,187 276,186 278,183 278,180 283,180 288,180 283,180 279,181 279,183 279,186 279,184 279,181 287,181 296,181 296,190 296,199 295,217 295,236 291,236 288,236 288,237 288,239 299,239 310,239 310,243 311,247 313,247 313,242 313,236 316,236 319,236 319,215 319,193 314,193 310,193 310,187 309,181 308,190 309,199 310,196 310,194 314,194 318,194 318,215 318,236 307,236 297,236 297,208 297,181 292,180 288,180 293,180 297,180 297,193 297,206 302,206 306,205 309,205 312,205 315,206 317,206 317,200 318,194 314,194 310,194 310,197 309,199 308,199 308,190 308,181 309,181 310,181 310,186 310,192 315,192 319,193 319,214 319,236 316,236 313,237 313,242 313,247 322,247 332,247 332,251 332,255 354,255 377,255 377,250 377,245 386,245 396,245 395,208 396,171 397,169 397,159 397,150 387,150 378,150 377,157 377,165 354,165 330,165 354,165 377,164 377,157 377,150 387,150 398,150 398,160 397,171 396,209 396,246 387,246 378,246 378,250 378,255 354,255 331,255 331,251').fill('none').stroke({ width: 1 })

  }

  @Input() buildingId: number;
  @Input() loadedFloorId: number = -1; //used to load ready map into component
  @Output() mapCreated = new EventEmitter<boolean>();
  displayGrid: boolean;
  patternRect;

  displayBackgroundImage: boolean;
  backgroundImage;
  imageHref = null;


  element;

  photoFile: File;
  saveWithBackgroundPhoto: boolean = true;
  placingSeatEnabled: boolean = false;
  movingPictureEnabled: boolean = true;
  floorNumber: number;
  floorDescription: string;
  removalMode: boolean = false;
  deleteKeyPressed: boolean;
  drawnMap;
  rooms: Room[];
  drawMode: string;
  drawingRightNow: boolean = false;
  createdRooms = Array();
  rects = Array();
  lines = Array();
  wallls = Array();

  constructor(private roomService: RoomService, private floorService: FloorServiceService) {
  }

  changeImage() {
    this.backgroundImage = this.drawnMap.image(this.imageHref, 760, 760).move(20, 20).draggable().back();
    this.displayBackgroundImage = true;
    (<HTMLInputElement>document.getElementById("displayBackgroundImageCheckBox")).checked = true;
  }

  ngOnInit() {
    this.displayMap();
    this.addEventHandlers();
  }

  addEventHandlers() {
    var self = this;
    document.onkeydown = function switchRemovalMode(e) {
      if (self.removalMode) return;
      if (e.keyCode == 68) {
        self.removalMode = true;
      }
    };
    document.onkeyup = function (e) {
      if (e.keyCode == 68) {
        self.removalMode = false;
      }
    };
  }

  switchOnRemovalMode() {
    this.drawnMap.off("mousedown");
    this.drawnMap.off("mouseup");
    this.removalMode = !this.removalMode;
  }

  movingImageOnOff() {
    this.movingPictureEnabled = !this.movingPictureEnabled;
    this.backgroundImage.draggable(this.movingPictureEnabled);
  }

  drawNewRoom() {
    this.drawMode = 'room';
    this.drawnMap.off("mousedown");
    this.drawnMap.off("mouseup");


    if (!this.drawingRightNow) {
      // grup defined to hold seats, room shape and circles at vertexes of room together
      var group = this.drawnMap.group();
      var poly = this.drawnMap.polygon().draw({ snapToGrid: defaultValues.gridSize }).fill({ color: '#4f4', opacity: '0.7' }).stroke({ width: 1 }).click(f => { if (this.removalMode) { var index = this.createdRooms.findIndex((x) => x.polygon.attr().id === poly.attr().id); this.createdRooms.splice(index, 1); group.remove(); } });
      this.drawingRightNow = true;
      var self = this;
      //function that handles end of drawing - 
      var stopDrawingAtEnter = function stopDrawingAtEnter(e) {
        if (e.keyCode == 13) {
          document.removeEventListener('keydown', stopDrawingAtEnter);
          poly.draw('done');
          poly.off('drawstart');
          self.drawingRightNow = false;
          var room = new MapCreatorComponent.RoomWithSeats;
          room.polygon = poly;
          room.stateChanged = "added";
          self.createdRooms.push(room);

          var oneCoordinateAtTime = self.createdRooms[self.createdRooms.length - 1].polygon.attr('points').trim().split(/[\s,]+/);
          //holds circles at vertexes of polygon
          var arrayOfCircles = Array();
          for (var j = 0; j <= (oneCoordinateAtTime.length - 2); j += 2) {
            var x = parseInt(oneCoordinateAtTime[j]);
            var y = parseInt(oneCoordinateAtTime[j + 1]);
            var pointie = self.drawnMap.circle(10).fill('#f06').move(x - 5, y - 5).draggable(function (x, y) {
              //function to move circles snapped to grid
              return {
                x: x - x % defaultValues.gridSize - 5,
                y: y - y % defaultValues.gridSize - 5
              };
            })
              //every time when circle is moved polygon needs to be replotted
              .on('dragmove.namespace', function (event) {
                var coords = '';
                arrayOfCircles.forEach(circle => {
                  coords = coords.concat(circle.attr('cx') + ',' + circle.attr('cy') + ' ')
                });
                poly.plot(coords);
                group.front();
                arrayOfCircles.forEach(circle => {
                  circle.front();
                });
              });

            group.add(pointie);
            arrayOfCircles.push(pointie);
          }



          group.add(poly);
          poly.click(function placeSeat(event) {
            //seat size hardcoded to 10x10 for now !!! alligned to grid: 10
            if (self.placingSeatEnabled) {
              var seatRect = self.drawnMap.rect(defaultValues.seatSize, defaultValues.seatSize).move((event.offsetX - event.offsetX % defaultValues.gridSize), (event.offsetY - event.offsetY % defaultValues.gridSize)).draggable((function (x, y) {
                //function to move seats snapped to grid
                return {
                  x: x - x % defaultValues.gridSize,
                  y: y - y % defaultValues.gridSize
                };
              }))
                .click(f => { if (this.removalMode) seatRect.remove(); this.removalMode = false; });;
              var seatVertex = { X: seatRect.attr().x, Y: seatRect.attr().y } as Vertex;
              var seat = { Vertex: seatVertex }
              room.seats.push(seat);
              group.add(seatRect);
            }
          })
        }
      };

      poly.on('drawstart', function (e) {
        document.addEventListener('keydown', stopDrawingAtEnter);
      });
      document.addEventListener('keydown', function cancelDrawingAtEscape(e) {
        if (e.keyCode == 27) {
          if (self.displayBackgroundImage && self.imageHref !== null) {
            self.backgroundImage.draggable();
          }
          document.removeEventListener('keydown', cancelDrawingAtEscape);
          document.removeEventListener('keydown', stopDrawingAtEnter);
          poly.draw('cancel');
          poly.off('drawstart');
          self.drawingRightNow = false;
        }
      });


    }
  }

  clearMap() {
    this.drawnMap.clear();
    this.lines.length = 0;
    this.rects.length = 0;
    this.createdRooms.length = 0;
    this.displayMap();
  }

  displayMap() {
    SVG.prepare();
    this.drawnMap = SVG.adopt(document.getElementById('createMap'));

    if (this.loadedFloorId != -1) {
      this.loadMap();
    }

  }

  chooseLine() {
    var self = this;
    this.drawMode = 'line';
    this.drawnMap.off("mousedown");
    this.drawnMap.off("mouseup");
    var wallando;
    var line = this.drawnMap.line().fill('none').stroke({ width: 3 }).mouseover(f => { if (this.removalMode) { var index = this.lines.findIndex((x) => x.attr().id === line.attr().id); wallando.StateChanged = "deleted"; this.lines.splice(index, 1); line.remove(); } });
    this.drawnMap.on(
      "mousedown",
      function (e) {
        line.draw(e, { snapToGrid: defaultValues.gridSize });
      },
      false
    );

    this.drawnMap.on(
      "mouseup",
      function (e) {
        line.draw("stop", e);
        self.lines.push(line);
        wallando = self.coordinatesToWall(line.attr('x1'), line.attr('y1'), line.attr('x2'), line.attr('y2'), "added", 0);
        self.wallls.push(wallando);
      },
      false
    );
  }

  chooseSquare() {
    var self = this;
    this.drawnMap.off("mousedown");
    this.drawnMap.off("mouseup");
    this.drawMode = 'square';
    var rect = this.drawnMap.rect().fill('none').stroke({ width: 3 }).mouseover(f => { if (this.removalMode) { rect.remove(); var index = this.rects.findIndex((x) => x.attr().id === rect.attr().id); this.rects.splice(index, 1) } });
    this.drawnMap.on(
      "mousedown",
      function (e) {
        rect.draw(e, { snapToGrid: defaultValues.gridSize });
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
    if (this.removalMode) {
      return
    }
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
    /*for (var i = 0; i < this.lines.length; i++) {
      linesVertexes.push(this.coordinatesToLine(this.lines[i].attr('x1'), this.lines[i].attr('y1'), this.lines[i].attr('x2'), this.lines[i].attr('y2')));
    }*/
    //parse svg.js rects to 
    /*for (var i = 0; i < this.rects.length; i++) {
      linesVertexes.push(...this.rectToLines(this.rects[i]));
    }*/ //TODO: squares to lines
    //polygon to lines
    var Rooms = Array();
    for (var i = 0; i < this.createdRooms.length; i++) {
      var Walls = Array();
      if (this.createdRooms[i].stateChanged != "modified") {
        var oneCoordinateAtTime = this.createdRooms[i].polygon.attr('points').trim().split(/[\s,]+/);
        for (var j = 0; j < (oneCoordinateAtTime.length - 2); j += 2) {
          Walls.push(this.coordinatesToLine(oneCoordinateAtTime[j], oneCoordinateAtTime[j + 1], oneCoordinateAtTime[j + 2], oneCoordinateAtTime[j + 3]));
        }
        Walls.push(this.coordinatesToLine(oneCoordinateAtTime[oneCoordinateAtTime.length - 2], oneCoordinateAtTime[oneCoordinateAtTime.length - 1], oneCoordinateAtTime[0], oneCoordinateAtTime[1]));
      } else {
        Walls.push(...this.createdRooms[i].Walls);
      }
      //push seats and Projects as empty
      var Seats = this.createdRooms[i].seats;
      var room: Room = { Walls, Seats, Id: this.createdRooms[i].RoomId, StateChanged: this.createdRooms[i].stateChanged } as Room;//!!! Seats
      Rooms.push(room);
    }

    if (this.saveWithBackgroundPhoto && this.backgroundImage != null)
      var floor: Floor = { Rooms, Walls: this.wallls, BuildingId: this.buildingId, Description: this.floorDescription, FloorNumber: this.floorNumber, XPhoto: 20, YPhoto: 20 } as Floor;
    else
      var floor: Floor = { Rooms, Walls: this.wallls, BuildingId: this.buildingId, Description: this.floorDescription, FloorNumber: this.floorNumber } as Floor;

    if (this.loadedFloorId == -1) {
      this.floorService.addFloor(floor).subscribe(data => {
        this.mapCreated.emit(true); this.loadedFloorId = data;
        if (this.saveWithBackgroundPhoto && this.backgroundImage != null) {
          this.floorService.uploadBackgroundPhoto(this.photoFile, data);
        }
      });

    } else {
      floor.Id = this.loadedFloorId;
      this.floorService.updateFloor(floor).subscribe(floorNumber => { this.mapCreated.emit(true); });
    }

    /*
    //clear map
    this.lines.length = 0;
    this.rects.length = 0;
    this.createdRooms.length = 0;
    this.drawnMap.clear();
    */
  }

  rectToLines(rect: Rect): Wall[] {
    var lines = Array();

    lines.push(this.coordinatesToLine(rect.attr('x'), rect.attr('y'), rect.attr('x') + rect.attr('width'), rect.attr('y')));
    lines.push(this.coordinatesToLine(rect.attr('x'), rect.attr('y'), rect.attr('x'), rect.attr('y') + rect.attr('height')));
    lines.push(this.coordinatesToLine(rect.attr('x') + rect.attr('width'), rect.attr('y'), rect.attr('x') + rect.attr('width'), rect.attr('y') + rect.attr('height')));
    lines.push(this.coordinatesToLine(rect.attr('x'), rect.attr('y') + rect.attr('height'), rect.attr('x') + rect.attr('width'), rect.attr('y') + rect.attr('height')));
    return lines;
  }

  coordinatesToLine(x1, y1, x2, y2): Wall {
    var X = x1;
    var Y = y1
    var StartVertex = { X, Y } as Vertex;

    X = x2;
    Y = y2;
    var EndVertex = { X, Y } as Vertex;

    var wall: Wall = { StartVertex, EndVertex } as Wall;
    return wall;
  }

  coordinatesToWall(x1, y1, x2, y2, StateChanged, Id): Wall {
    var X = x1;
    var Y = y1
    var StartVertex = { X, Y } as Vertex;

    X = x2;
    Y = y2;
    var EndVertex = { X, Y } as Vertex;

    var wall: Wall = { StartVertex, EndVertex, StateChanged, Id } as Wall;
    return wall;
  }


  loadMap() {
    let floor;
    this.floorService.getFloor(this.loadedFloorId)
      .subscribe(
        Floor => {
          floor = Floor;
          this.floorNumber = Floor.FloorNumber;
          this.floorDescription = Floor.Description;
          floor.Rooms.forEach(room => {
            var roomie = new MapCreatorComponent.RoomWithSeats;
            roomie.RoomId = room.Id;
            //concatenate room lines into polygon coordinates
            var arra = '';
            room.Walls.forEach(wall => {
              /*test*/
              let StartVertex = { X: wall.StartVertex.X, Y: wall.StartVertex.Y } as Vertex;
              let EndVertex = { X: wall.EndVertex.X, Y: wall.EndVertex.Y } as Vertex;
              let Wallie = { Id: wall.Id, StartVertex, EndVertex };
              roomie.Walls.push(Wallie);
              /*stop test */
              arra = arra.concat(wall.StartVertex.X + ',' + wall.StartVertex.Y + ' ')
            });

            var poly = this.drawnMap
              .polygon(arra).fill('#999')
              .stroke({ width: 2 })
              .click(f => { if (this.removalMode) { var index = this.createdRooms.findIndex((x) => x.polygon.attr().id === poly.attr().id); this.createdRooms[index].stateChanged = "deleted"; group.remove(); } });//!!!

            var group = this.drawnMap.group();
            roomie.polygon = poly;

            var oneCoordinateAtTime = poly.attr('points').trim().split(/[\s,]+/);
            //holds circles at vertexes of polygon
            var arrayOfCircles = Array();
            for (var j = 0; j <= (oneCoordinateAtTime.length - 2); j += 2) {
              var x = parseInt(oneCoordinateAtTime[j]);
              var y = parseInt(oneCoordinateAtTime[j + 1]);
              let pointNumber = j/2;
              var pointie = this.drawnMap.circle(10).fill('#f06').move(x - 5, y - 5).draggable(function (x, y) {
                //function to move circles snapped to grid
                /*test */
                if(pointNumber == 0) {
                  roomie.Walls[0].StartVertex.X = x - x % defaultValues.gridSize;
                  roomie.Walls[0].StartVertex.Y = y - y % defaultValues.gridSize;
                  roomie.Walls[oneCoordinateAtTime.length/2 - 1].EndVertex.X = x - x % defaultValues.gridSize;
                  roomie.Walls[oneCoordinateAtTime.length/2 - 1].EndVertex.Y = y - y % defaultValues.gridSize;
                  roomie.Walls[0].StateChanged = "modified";
                  roomie.Walls[oneCoordinateAtTime.length/2 - 1].StateChanged = "modified";
                } else {
                  roomie.Walls[pointNumber].StartVertex.X = x - x % defaultValues.gridSize;
                  roomie.Walls[pointNumber].StartVertex.Y = y - y % defaultValues.gridSize;
                  roomie.Walls[pointNumber-1].EndVertex.X = x - x % defaultValues.gridSize;
                  roomie.Walls[pointNumber-1].EndVertex.Y = y - y % defaultValues.gridSize;
                  roomie.Walls[pointNumber].StateChanged = "modified";
                  roomie.Walls[pointNumber-1].StateChanged = "modified";
                }
                roomie.stateChanged = "modified";
                /*if (wallModified != 0 && wallModified != oneCoordinateAtTime.length/2 - 1) {
                  roomie.Walls[wallModified].StateChanged = "modified";
                  roomie.Walls[wallModified+1].StateChanged = "modified";                  
                } else if(wallModified == 0){
                  roomie.Walls[0].StateChanged = "modified";
                  roomie.Walls[roomie.Walls.length - 1].StateChanged = "modified";
                } else if(wallModified == oneCoordinateAtTime.length/2 - 1)
                {
                  roomie.Walls[0].StateChanged = "modified";
                  roomie.Walls[roomie.Walls.length - 1].StateChanged = "modified";
                }*/
                /*stop test */
                return {
                  x: x - x % defaultValues.gridSize - 5,
                  y: y - y % defaultValues.gridSize - 5
                };
              })
                //every time when circle is moved polygon needs to be replotted
                .on('dragmove.namespace', function (event) {
                  var coords = '';
                  arrayOfCircles.forEach(circle => {
                    coords = coords.concat(circle.attr('cx') + ',' + circle.attr('cy') + ' ')
                  });
                  poly.plot(coords);




                  group.front();
                  arrayOfCircles.forEach(circle => {
                    circle.front();
                  });
                });
              group.add(pointie);
              arrayOfCircles.push(pointie);
            }

            group.add(poly);

            var self = this;
            let seat;
            poly.click(function placeSeat(event) {
              if (self.placingSeatEnabled) {
                roomie.stateChanged = "modified";
                var seatRect = self.drawnMap.rect(defaultValues.seatSize, defaultValues.seatSize).move((event.offsetX - event.offsetX % defaultValues.gridSize), (event.offsetY - event.offsetY % defaultValues.gridSize)).draggable((function (x, y) {
                  //function to move seats snapped to grid
                  return {
                    x: x - x % defaultValues.gridSize,
                    y: y - y % defaultValues.gridSize
                  };
                }))
                  .click(f => { if (this.removalMode) { seatRect.remove(); } });
                var seatVertex = { X: seatRect.attr().x, Y: seatRect.attr().y } as Vertex;
                seat = { Vertex: seatVertex, StateChanged: "added" };
                roomie.seats.push(seat);
                group.add(seatRect);
              }
            })

            room.Seats.forEach(seat => {
              var seatle = this.drawnMap
                .rect(defaultValues.seatSize, defaultValues.seatSize)
                .move(seat.Vertex.X, seat.Vertex.Y)
                .fill('#123')
                .stroke({ width: 0 })
                .draggable((function (x, y) {
                  //function to move seats snapped to grid
                  roomie.stateChanged = "modified";
                  seat.StateChanged = "modified";
                  seat.Vertex.X = x - x % defaultValues.gridSize;
                  seat.Vertex.Y = y - y % defaultValues.gridSize;
                  return {
                    x: x - x % defaultValues.gridSize,
                    y: y - y % defaultValues.gridSize
                  };
                }))
                .click(f => { if (this.removalMode) { seatle.remove(); this.removalMode = false; seat.StateChanged = "deleted"; roomie.stateChanged = "modified" } });
              var seatVertex = { X: seat.Vertex.X, Y: seat.Vertex.Y } as Vertex;
              roomie.seats.push(seat);
              group.add(seatle);
            });


            this.createdRooms.push(roomie);
          });
          floor.Walls.forEach(wall => {
            //drawing lines
            let wallando = this.coordinatesToWall(wall.StartVertex.X, wall.StartVertex.Y, wall.EndVertex.X, wall.EndVertex.Y, "null", wall.Id);
            var line = this.drawnMap
              .line(wall.StartVertex.X + ", " + wall.StartVertex.Y + ", " + wall.EndVertex.X + ", " + wall.EndVertex.Y)
              .stroke({ width: 2 })
              .mouseover(f => { if (this.removalMode) { var index = this.lines.findIndex((x) => x.attr().id === line.attr().id); this.lines.splice(index, 1); line.remove(); wallando.StateChanged = "deleted" } });
            //this.lines.push(line);
            this.wallls.push(wallando);
          });
        });


  }

  onSelectFile(event) { // called each time file input changes
    if (event.target.files && event.target.files[0]) {
      if (this.backgroundImage)
        this.backgroundImage.remove();
      this.photoFile = null;
      this.imageHref = null;
      var reader = new FileReader();
      this.photoFile = event.target.files[0];
      reader.readAsDataURL(event.target.files[0]); // read file as data url
      reader.onload = (event) => { // called once readAsDataURL is completed
        this.imageHref = (event.target as any).result;
        this.changeImage();
      }
    }
  }

  gridOnOff() {
    this.displayGrid = !this.displayGrid;
    if (this.displayGrid) {
      var pattern = this.drawnMap.pattern(defaultValues.gridSize, defaultValues.gridSize, function (add) {
        add.rect(defaultValues.seatSize, defaultValues.seatSize).fill('none').stroke({ width: 1, color: '#005' });
      });
      pattern.fill('none');
      this.patternRect = this.drawnMap.rect(800, 800);
      this.patternRect.fill(pattern);
      this.patternRect.back();
      if (this.imageHref !== null) {
        this.patternRect.forward();
      }
    } else if (this.patternRect) {
      this.patternRect.remove();
    }
  }

  backgroundImageOnOff() {
    this.displayBackgroundImage = !this.displayBackgroundImage;
    if (this.displayBackgroundImage && this.imageHref !== null) {
      this.changeImage();
    } else if (this.imageHref !== null) {
      this.backgroundImage.remove();
    }
  }

  placeSeatsOnOff() {
    this.placingSeatEnabled = !this.placingSeatEnabled;
    this.drawnMap.off("mousedown");
    this.drawnMap.off("mouseup");
  }

  static RoomWithSeats = class {
    RoomId: number;
    polygon;
    seats = Array();
    stateChanged: string;
    Walls = Array();
  }

}

