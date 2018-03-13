import { SharedService } from './services/shared.service';
import { HttpErrorHandler } from './services/http-error-handler.service';
import { MessageService } from './services/message.service';
import { RoomService } from './services/room.service';

import { BrowserModule } from '@angular/platform-browser';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { DisplayedMapComponent } from './displayed-map/displayed-map.component';
import { RoomComponent } from './room/room.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { MapCreatorComponent } from './map-creator/map-creator.component';
import { ListingComponent } from './listing/listing.component';
import { PersonCardComponent } from './person-card/person-card.component';
import { EmployeeService } from './services/employee.service';
import { SearchComponent } from './search/search.component';
import { ProjectCardComponent } from './project-card/project-card.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';


@NgModule({
  declarations: [
    AppComponent,
    DisplayedMapComponent,
    RoomComponent,
    MapCreatorComponent,
    ListingComponent,
    PersonCardComponent,
    SearchComponent,
    ProjectCardComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    BrowserModule,
    MDBBootstrapModule.forRoot(),
    FormsModule,
    InfiniteScrollModule
  ],
  providers: [
    HttpErrorHandler,
    MessageService,
    RoomService,
    EmployeeService,
    SharedService],
  bootstrap: [AppComponent],
  schemas: [ NO_ERRORS_SCHEMA ]
})
export class AppModule { }
