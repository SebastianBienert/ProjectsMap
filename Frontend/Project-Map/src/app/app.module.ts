import { FloorServiceService } from './services/floor-service.service';
import { SharedService } from './services/shared.service';
import { HttpErrorHandler } from './services/http-error-handler.service';
import { MessageService } from './services/message.service';
import { RoomService } from './services/room.service';

import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { NgModule, NO_ERRORS_SCHEMA } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import "rxjs/Rx";
import { EmployeeDetailsModule }  from './employee-details/employee-details.module';

import { AppComponent } from './app.component';
import { DisplayedMapComponent } from './displayed-map/displayed-map.component';
import { InsertFormsComponent } from './insert-forms/insert-forms.component';
import { EmployeeComponent } from './insert-forms/employee/employee.component';
import { ProjectComponent } from './insert-forms/project/project.component';
import { MDBBootstrapModule } from 'angular-bootstrap-md';
import { MapCreatorComponent } from './map-creator/map-creator.component';
import { ListingComponent } from './listing/listing.component';
import { PersonCardComponent } from './person-card/person-card.component';
import { EmployeeService } from './services/employee.service';
import { SearchComponent } from './search/search.component';
import { ProjectCardComponent } from './project-card/project-card.component';
import { InfiniteScrollModule } from 'ngx-infinite-scroll';
import { ReactiveFormsModule }  from '@angular/forms';
import { MapNavigatorComponent } from './map-navigator/map-navigator.component';
import { TechnologyService } from './services/technology.service';
import { TagInputModule } from 'ngx-chips';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // this is needed!
import { ProjectService } from './services/project.service';
import { ManagementPageComponent } from './management-page/management-page.component';
import { SidebarComponent } from './sidebar/sidebar.component';
import { RouterModule, Routes } from '@angular/router';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { PageNotFoundComponent } from './not-found.component';
import { EmployeeDetailComponent } from './employee-details/employee-detail.component';
import { Globals } from './globals';

const routes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full'},
  { path: 'main', 
      component: MainLayoutComponent,
      children: [
        {path: 'displayMap', component: DisplayedMapComponent, outlet: 'center'}, 
        {path: ':id', component: EmployeeDetailComponent, outlet: 'right'}, 
        {path: '', component: DisplayedMapComponent, outlet: 'center'}, 
        { path: '**', component: PageNotFoundComponent, outlet: 'center' },
        { path: '**', component: PageNotFoundComponent, outlet: 'right' }
      ]
    },
  { path: 'managementPage', 
    component: ManagementPageComponent,
    children: [
      {path: '', component: MapNavigatorComponent}, 
      {path: 'projects', component: ProjectComponent}, 
      {path: 'employees', component: EmployeeComponent}, 
      {path: 'mapCreator', component: MapNavigatorComponent}, 
  ]},
  { path: '**', component: PageNotFoundComponent }

]


@NgModule({
  declarations: [
    AppComponent,
    DisplayedMapComponent,
    InsertFormsComponent,
    EmployeeComponent,
    ProjectComponent,
    MapCreatorComponent,
    SearchComponent,
    ListingComponent,
    PersonCardComponent,
    SearchComponent,
    MapNavigatorComponent,
    ProjectCardComponent,
    ManagementPageComponent,
    SidebarComponent,
    MainLayoutComponent,
    PageNotFoundComponent
  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule,
    CommonModule,
    HttpClientModule,
    FormsModule,
    MDBBootstrapModule.forRoot(),
    RouterModule.forRoot(routes),
    InfiniteScrollModule,
    TagInputModule, 
    BrowserAnimationsModule,
    EmployeeDetailsModule
  ],
  providers: [
    HttpErrorHandler,
    MessageService,
    RoomService,
    EmployeeService,
    ProjectService,
    SharedService,
    FloorServiceService,
    TechnologyService,
    Globals],
  bootstrap: [AppComponent],
  schemas: [ NO_ERRORS_SCHEMA ]
})
export class AppModule { }
