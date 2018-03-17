import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { HandleError, HttpErrorHandler } from './http-error-handler.service';

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/json',
    'Authorization': 'my-auth-token'
  })
};

@Injectable()
export class ProjectService {
  projectUrl = 'http://localhost:58923/api/project';  // For localhosted webapi
  // employeeUrl = 'https://projectsmapwebapi.azurewebsites.net/api/project';  // For localhosted webapi
 
  private handleError: HandleError;
 
  constructor(private http: HttpClient,
    httpErrorHandler: HttpErrorHandler) { 
      this.handleError = httpErrorHandler.createHandleError('TechnologyService');
    }

   public addProject(project){
      this.http.post(this.projectUrl, project).subscribe(
        res => {
          console.log(res);
        },
        err => {
          console.log("Error occured" + JSON.stringify(err));
        }
      );
    }

}
