import { HttpErrorHandler } from './services/http-error-handler.service';
import { MessageService } from './services/message.service';
import { RoomService } from './services/room.service';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { DrawingBoardComponent } from './drawing-board/drawing-board.component';
import { RoomComponent } from './room/room.component';


@NgModule({
  declarations: [
    AppComponent,
    DrawingBoardComponent,
    RoomComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
  ],
  providers: [
    HttpErrorHandler,
    MessageService,
    RoomService],
  bootstrap: [AppComponent]
})
export class AppModule { }
