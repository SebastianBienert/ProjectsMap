import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormControl } from '@angular/forms';
import { AppUserAuth } from './../security/app-user-auth';
import { SecurityService } from './../security/security.service';
import { EmployeeService } from './../services/employee.service';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { Floor } from './../common-interfaces/floor';
import { FloorServiceService } from './../services/floor-service.service';
import { Vertex } from './../common-interfaces/vertex';
import { async } from '@angular/core/testing';
import { RoomService } from './../services/room.service';
import { Scale, Doc, PointArray, Rect } from './../../../node_modules/svg.js/svg.js.d';
import { Component, OnInit, Input, Output, SimpleChange, OnChanges, EventEmitter, OnDestroy, ViewChild } from '@angular/core';
import { Room } from '../common-interfaces/room';
import { colors, styling } from './svgcolors';
import { Subscription } from 'rxjs';
declare const SVG: any;
@Component({
  selector: 'app-displayed-map',
  templateUrl: './displayed-map.component.html',
  styleUrls: ['./displayed-map.component.css'],
})
export class DisplayedMapComponent implements OnInit, OnChanges, OnDestroy {
  @ViewChild('content') modalZiom;
  subscription = new Subscription();
  backgroundImage: Blob;
  rooms: Room[];
  floor: Floor;
  seats = new Array();
  selectedEmployeeRoomId: number = -1;
  selectedEmployeeSeatId: number = -1;
  selectedEmployeeId: number = -1;
  securityObject: AppUserAuth = null;
  term = new FormControl();
  svgPhoto;
  modalReference : any;

  @Input() floorToDisplay: number;
  @Output() mapChanged = new EventEmitter<{ floorId:number, buildingId: number }>();
  drawnMap;
  changeLog: string[] = [];
  constructor(private roomService: RoomService, private floorService: FloorServiceService, private route: ActivatedRoute,
    private router: Router, private employeeService: EmployeeService, private securityService: SecurityService, private modalService: NgbModal) { }

  ngOnInit() {
    this.securityObject = this.securityService.securityObject;
    // this.drawnMap = SVG.adopt(document.getElementById('svg')).panZoom({zoomMin: 0.5, zoomMax: 20});
    this.drawnMap = SVG('svg').size(800, 800).panZoom({ zoomMin: 0.5, zoomMax: 10 });
    //this.drawnMap.circle(100).move(350, 350);
    this.route.params.subscribe(params => this.selectedEmployeeId = + params['id']);
    //this.getFloor();
    if (this.selectedEmployeeId > 0) {
      this.route.paramMap
        .switchMap((params: ParamMap) =>
          this.employeeService.getEmployeeLocationInfo(+params.get('id')))
        .subscribe(EmployeeLocationInfo => {
         // console.log("zioom " + )
          this.mapChanged.emit({floorId:EmployeeLocationInfo.FloorId, buildingId:EmployeeLocationInfo.EmployeeBuildingId});
          this.floorToDisplay = EmployeeLocationInfo.FloorId;
          this.selectedEmployeeRoomId = EmployeeLocationInfo.RoomId;
          this.selectedEmployeeSeatId = EmployeeLocationInfo.SeatId;
          this.getFloor();
        });
    }
  }

  ngOnDestroy(): void {
   
  }

  getFloor(): void {
    setTimeout(() => {
      this.open(this.modalZiom);
    })
    
    this.subscription.unsubscribe();
    this.backgroundImage = null;
    this.svgPhoto = null;
    this.floor = null;
    if(this.drawnMap != null)
      this.drawnMap.clear();
    this.floorService.getFloor(this.floorToDisplay)
      .subscribe(
        Floor => {
          if(Floor.XPhoto == null) this.modalReference.close();
          this.floor = Floor;
          console.log("Moje pietro" + Floor.YPhoto)
          if (Floor.XPhoto != null) {
            this.subscription  = this.floorService.getFloorPhoto(this.floorToDisplay).subscribe(
              photo => {
                this.modalReference.close()
                let self = this;
                var reader = new FileReader();
                reader.readAsDataURL(photo);
                
                reader.onloadend = function () {
                  self.backgroundImage = reader.result;
                  self.displayMap();
                }
                
              });
          } else {
            this.displayMap();
          }
        });
  }

  getFloorByEmployeeId(employeeId: number) {
    console.log("request2");
    this.floorService.getFloorByEmployeeId(employeeId)
      .subscribe(
        Floor => {
          this.floor = Floor;
          this.displayMap();
        });
  }


  ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
    for (let propName in changes) {
      let changedProp = changes[propName];
      this.floorToDisplay = changedProp.currentValue;
      console.log("lool");
      this.backgroundImage = null;
      this.svgPhoto = null;
      this.subscription.unsubscribe();
      console.log("request3");
      this.getFloor();
    }
  }

  displayMap() {
    
    this.drawnMap.clear();
    if (this.backgroundImage != null) {
      //console.log(this.backgroundImage);
      this.svgPhoto = this.drawnMap.image(this.backgroundImage, 760, 760).move(this.floor.XPhoto, this.floor.YPhoto).back();
    }
    var el = document.getElementById("svg");
    var rect = el.getBoundingClientRect(); // get the bounding rectangle
    let viewboxSize = '-10 -150 ' + rect.width + ' ' + rect.width;
    this.drawnMap.attr('viewBox', viewboxSize);
    this.drawnMap.zoom(1, { x: rect.width / 2, y: rect.width / 2 });

    this.floor.Rooms.forEach(room => {
      //concatenate room vertices into polygon coordinates
      var arra = '';
      if (room.Walls.length != 0) {
        room.Walls.forEach(wall => {
          arra = arra.concat(wall.StartVertex.X + ',' + wall.StartVertex.Y + ' ')
        });
      }
      //drawing room with mouseover and mouseout events { color: '#f06', opacity: 0.6 }
      this.drawnMap
        .polygon(arra).fill({color:this.selectedEmployeeRoomId === room.Id ? colors.roomWithMouseOverColor : colors.roomColor, opacity: 0.6})
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
      this.drawnMap
        .line(wall.StartVertex.X + ", " + wall.StartVertex.Y + ", " + wall.EndVertex.X + ", " + wall.EndVertex.Y)
        .stroke({ width: 2 });
    });

    var self = this;
    this.floor.Rooms.forEach(room => {
      room.Seats.forEach(seat => {
        //determine color
        let seatColor = colors.seatColor;
        if (this.selectedEmployeeSeatId === seat.Id) seatColor = colors.selectedSeatColor;
        if (seat.DeveloperId === null) seatColor = colors.emptySeatColor;


        this.drawnMap
          .rect(10, 10)
          .move(seat.Vertex.X, seat.Vertex.Y)
          .fill(seatColor)
          .stroke({ width: 0 })
          .click(function () {
            if (!(seat.DeveloperId === null))
              self.router.navigate(['/main',{outlets: {right: ['user', seat.DeveloperId], center: [seat.DeveloperId]} }]);
            else {
              //check if there exists seatQuestionMessageBox, don't show new one if so
              let seatQuestionMessageBox = SVG.get('seatQuestionMessageBox');
              if (!(seatQuestionMessageBox === null)) return;

              //'messageBox' with question about taking new seat
              var messageBoxGroup = self.drawnMap.group();
              let rectangleQuestion = self.drawnMap
                .rect(300, 100)
                .move(seat.Vertex.X + 10, seat.Vertex.Y)
                .fill(colors.seatMessageColor)
                .radius(10)
                .attr('id', 'seatQuestionMessageBox');
              let questionText = self.drawnMap.text('Take this seat?').move(seat.Vertex.X + 10 + 300 / 2, seat.Vertex.Y);
              questionText.font({
                family: 'Helvetica'
                , size: 20
                , anchor: 'middle'
                , leading: '1.5em'
              });

              messageBoxGroup.add(rectangleQuestion);
              messageBoxGroup.add(questionText);

              let rectangleYeah = self.drawnMap
                .rect(100, 40)
                .move(seat.Vertex.X + 10 + 20, seat.Vertex.Y + 50)
                .fill(colors.seatAnswersColor)
                .mouseout(function () {
                  this.fill(colors.seatAnswersColor)
                })
                .mouseover(function () {
                  this.fill(colors.seatAnswerHoverColor);
                })
                .radius(10)
                .click(function () {
                  self.floorService.assignNewSeat(seat.Id, self.securityObject.userId).subscribe(ans => { messageBoxGroup.remove(); self.getFloor() });
                });

              let answerYeahText = self.drawnMap.text('Yes').move(seat.Vertex.X + 10 + 20 + 100 / 2, seat.Vertex.Y + 50)
                .mouseout(function () {
                  rectangleYeah.fill(colors.seatAnswersColor)
                })
                .mouseover(function () {
                  rectangleYeah.fill(colors.seatAnswerHoverColor);
                })
                .click(function () {
                  self.floorService.assignNewSeat(seat.Id, self.securityObject.userId).subscribe(ans => { messageBoxGroup.remove(); self.getFloor() });
                });
              answerYeahText.font({
                family: 'Helvetica'
                , size: 20
                , anchor: 'middle'
                , leading: '1.5em'
                /*, stroke: '#fff'
                , fill: '#fff'*/
              });
              messageBoxGroup.add(rectangleYeah);
              messageBoxGroup.add(answerYeahText);


              let rectangleNope = self.drawnMap
                .rect(100, 40)
                .move(seat.Vertex.X + 190, seat.Vertex.Y + 50)
                .fill(colors.seatAnswersColor)
                .mouseout(function () {
                  this.fill(colors.seatAnswersColor)
                })
                .mouseover(function () {
                  this.fill(colors.seatAnswerHoverColor);
                })
                .radius(10)
                .click(function () { messageBoxGroup.remove() });

              let answerNopeText = self.drawnMap.text('No').move(seat.Vertex.X + 240, seat.Vertex.Y + 50)
                .mouseout(function () {
                  rectangleNope.fill(colors.seatAnswersColor)
                })
                .mouseover(function () {
                  rectangleNope.fill(colors.seatAnswerHoverColor);
                })
                .click(function () { messageBoxGroup.remove() });
              answerNopeText.font({
                family: 'Helvetica'
                , size: 20
                , anchor: 'middle'
                , leading: '1.5em'
              });

              messageBoxGroup.add(rectangleNope);
              messageBoxGroup.add(answerNopeText);
            }
          })
      });
    });
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
