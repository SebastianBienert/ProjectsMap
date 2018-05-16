import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';
import { FormsModule }    from '@angular/forms';

import { EmployeeDetailComponent }  from './employee-detail.component';

@NgModule({
  imports: [
    CommonModule,
    FormsModule
  ],
  declarations: [
    EmployeeDetailComponent
  ]
})
export class EmployeeDetailsModule {}
