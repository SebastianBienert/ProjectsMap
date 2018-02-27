import { HandleError } from './../services/http-error-handler.service';

import { RoomService } from './../services/room.service';
import { Room } from './../common-interfaces/room';
import { Vertex } from './../common-interfaces/vertex';

import { Component, OnInit } from '@angular/core';


@Component({
  selector: 'app-Room',
  templateUrl: './Room.component.html',
  providers: [ RoomService ],
  styleUrls: ['./Room.component.css']
})
export class RoomComponent implements OnInit {
  rooms: Room [];
  private handleError: HandleError;

  constructor(private roomService: RoomService) { }
  ngOnInit() {
    this.getRooms();
  }
  getRooms(): void {
    this.roomService.getRooms()
      .subscribe(Rooms => this.rooms = Rooms);
  }
}
