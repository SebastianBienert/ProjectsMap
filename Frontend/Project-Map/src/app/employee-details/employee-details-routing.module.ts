import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EmployeeListComponent }    from './employee-list.component';
import { EmployeeDetailComponent }  from './employee-detail.component';

const employeeRoutes: Routes = [
  { path: 'employee',  component: EmployeeListComponent },
  { path: 'employee/:id', component: EmployeeDetailComponent }
];

@NgModule({
  imports: [
    RouterModule.forChild(employeeRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class EmployeeRoutingModule { }
