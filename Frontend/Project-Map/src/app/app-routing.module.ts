import { NgModule }             from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { EmployeeListComponent }  from './employee-details/employee-list.component';
import { PageNotFoundComponent }    from './not-found.component';

const appRoutes: Routes = [
  {
    path: '',
    component: EmployeeListComponent,
  },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(
      appRoutes,
      {
        enableTracing: true // <-- debugging purposes only
      }
    )
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }