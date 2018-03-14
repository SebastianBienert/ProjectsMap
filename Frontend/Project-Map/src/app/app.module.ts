import { SharedService } from './services/shared.service';
import { HttpErrorHandler } from './services/http-error-handler.service';
import { MessageService } from './services/message.service';
import { RoomService } from './services/room.service';

import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import "rxjs/Rx";
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { DisplayedMapComponent } from './displayed-map/displayed-map.component';
import { InsertFormsComponent } from './insert-forms/insert-forms.component';
import { EmployeeComponent } from './insert-forms/employee/employee.component';
import { ProjectComponent } from './insert-forms/project/project.component';
import { TechnologyComponent } from './insert-forms/technology/technology.component';
import { RoomComponent } from './room/room.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { MapCreatorComponent } from './map-creator/map-creator.component';
import { ListingComponent } from './listing/listing.component';
import { PersonCardComponent } from './person-card/person-card.component';
import { EmployeeService } from './services/employee.service';
import { SearchComponent } from './search/search.component';
import { TechnologyService } from './services/technology.service';


@NgModule({
  declarations: [
    AppComponent,
    DisplayedMapComponent,
    RoomComponent,
    InsertFormsComponent,
    EmployeeComponent,
    ProjectComponent,
    TechnologyComponent,
    MapCreatorComponent,
    SearchComponent,
    ListingComponent,
    PersonCardComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    BrowserModule,
    MDBBootstrapModule.forRoot(),
    FormsModule
  ],
  providers: [
    HttpErrorHandler,
    MessageService,
    RoomService,
    EmployeeService,
    SharedService,
    TechnologyService],
  bootstrap: [AppComponent],
  schemas: [ NO_ERRORS_SCHEMA ]
})
export class AppModule { }
