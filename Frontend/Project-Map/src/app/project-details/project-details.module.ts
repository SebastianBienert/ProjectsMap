import { NgModule }       from '@angular/core';
import { CommonModule }   from '@angular/common';
import { FormsModule }    from '@angular/forms';
import { ProjectDetailComponent }  from './project-detail.component';
import { RouterModule, Routes } from '@angular/router';
import { ManagementPageComponent } from '../management-page/management-page.component';
import { EditProjectComponent } from '../edit-project/edit-project.component';
import { AuthGuard } from '../security/auth.guard';


const routes: Routes = [
  { path: '', redirectTo: '/main', pathMatch: 'full'},
  { path: 'managementPage', 
    component: ManagementPageComponent,
    children: [
      {path: 'editProjects/:id', component: EditProjectComponent,
      canActivate: [AuthGuard],
             data: { claimType: 'canWriteProjects' }}, 
  ]}
]

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes)
  ],
  declarations: [
    ProjectDetailComponent
  ]
})
export class ProjectDetailsModule {}
