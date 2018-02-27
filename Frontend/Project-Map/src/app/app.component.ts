import { RoomComponent } from './room/room.component';
import { Component, ViewChild } from '@angular/core';
import { DrawingBoardComponent } from './drawing-board/drawing-board.component';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Projects map';
}
