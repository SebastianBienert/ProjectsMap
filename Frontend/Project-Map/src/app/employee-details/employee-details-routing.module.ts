import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EmployeeDetailComponent }  from './employee-detail.component';

const employeeRoutes: Routes = [
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
