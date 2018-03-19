import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';
import { FormsModule }    from '@angular/forms';

import { EmployeeListComponent }    from './employee-list.component';
import { EmployeeDetailComponent }  from './employee-detail.component';

import { EmployeeRoutingModule } from './employee-details-routing.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    EmployeeRoutingModule
  ],
  declarations: [
    EmployeeListComponent,
    EmployeeDetailComponent
  ]
})
export class EmployeeDetailsModule {}
