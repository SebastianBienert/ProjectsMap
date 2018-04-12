import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators/tap';

import { AppUserAuth } from './app-user-auth';
import { AppUser } from './app-user';

const API_URL = "http://localhost:58923/oauth/";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/x-www-form-urlencoded',

  })
};


@Injectable()
export class SecurityService {
  securityObject: AppUserAuth = new AppUserAuth();

  constructor(private http: HttpClient) { }

  login(entity: AppUser): Observable<AppUserAuth> {
    // Initialize security object
    this.resetSecurityObject();

    let body = new URLSearchParams();
    body.set('username', entity.userName);
    body.set('password', entity.password);
    body.set('grant_type', 'password');

    return this.http.post<AppUserAuth>("http://localhost:58923/oauth/token",
      body.toString(), httpOptions).pipe(
        tap(resp => {
          console.log(resp);
          // Use object assign to update the current object
          // NOTE: Don't create a new AppUserAuth object
          //       because that destroys all references to object
          Object.assign(this.securityObject, resp);
          // Store into local storage
          localStorage.setItem("bearerToken",
            this.securityObject.access_token);
        }));
  }

  logout(): void {
    this.resetSecurityObject();
  }

  resetSecurityObject(): void {
    this.securityObject.userName = "";
    this.securityObject.access_token = "";
    this.securityObject.isAuthenticated = false;

    this.securityObject.canAccessProducts = false;
    this.securityObject.canAddProduct = false;
    this.securityObject.canSaveProduct = false;
    this.securityObject.canAccessCategories = false;
    this.securityObject.canAddCategory = false;

    localStorage.removeItem("bearerToken");
  }
}