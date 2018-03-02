import { HttpErrorHandler } from './services/http-error-handler.service';
import { MessageService } from './services/message.service';
import { RoomService } from './services/room.service';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { DisplayedMapComponent } from './displayed-map/displayed-map.component';
import { RoomComponent } from './room/room.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { MapCreatorComponent } from './map-creator/map-creator.component';


@NgModule({
  declarations: [
    AppComponent,
    DisplayedMapComponent,
    RoomComponent,
    MapCreatorComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserModule,
    MDBBootstrapModule.forRoot()
  ],
  providers: [
    HttpErrorHandler,
    MessageService,
    RoomService],
  bootstrap: [AppComponent],
  schemas: [ NO_ERRORS_SCHEMA ]
})
export class AppModule { }
