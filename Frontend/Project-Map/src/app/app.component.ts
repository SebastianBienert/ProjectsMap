import { RoomComponent } from './room/room.component';
import { Component, ViewChild } from '@angular/core';
import { DisplayedMapComponent } from './displayed-map/displayed-map.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { MDBBootstrapModule } from 'angular-bootstrap-md';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Projects map';


}
