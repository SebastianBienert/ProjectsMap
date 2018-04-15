import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators/tap';

import { AppUserAuth } from './app-user-auth';
import { AppUser } from './app-user';
import * as JWT from 'jwt-decode';
import { AppUserClaim } from './app-user-claim';

const API_URL = "http://localhost:58923/oauth/";

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/x-www-form-urlencoded',

  })
};



@Injectable()
export class SecurityService {
  securityObject: AppUserAuth = new AppUserAuth();

  constructor(private http: HttpClient) {
    
    let secObject = localStorage.getItem("bearerToken");
    console.log(secObject);
    if(secObject !== null)
    {
      this.mapResponse(secObject);
    }
   }

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
          this.mapResponse(resp.access_token);
          localStorage.setItem("bearerToken",
            this.securityObject.access_token);

          localStorage.setItem("securityObject",
          JSON.stringify(this.securityObject));
        }));
  }

  mapResponse(token): void {
    let decodedToken = JWT(token);
    
    this.securityObject.access_token = token;
    //delete token["access_token"];
    this.securityObject.isAuthenticated = true;
    this.securityObject.userName = decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
    delete decodedToken["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
    for(let el in decodedToken)
    {
      this.securityObject.claims.push(Object.assign(new AppUserClaim(), {
        claimType : el,
        claimValue : decodedToken[el]
      }))
    }
  }

  logout(): void {
    this.resetSecurityObject();
  }



  resetSecurityObject(): void {
    this.securityObject.userName = "";
    this.securityObject.access_token = "";
    this.securityObject.isAuthenticated = false;

    this.securityObject.claims = [];
    localStorage.removeItem("bearerToken");
  }

  // *hasClaim="'claimType'"
  // *hasClaim="'claimType:value'"
  // *hasClaim="['claimType1','claimType2:value','claimType3']"
  hasClaim(claimType: any, claimValue?: any) {
    let ret: boolean = false;

    // See if an array of values was passed in.
    if (typeof claimType === "string") {
      ret = this.isClaimValid(claimType, claimValue);
    }
    else {
      let claims: string[] = claimType;
      if (claims) {
        for (let index = 0; index < claims.length; index++) {
          ret = this.isClaimValid(claims[index]);
          // If one is successful, then let them in
          if (ret) {
            break;
          }
        }
      }
    }

    return ret;
  }

  private isClaimValid(claimType: string, claimValue?: string): boolean {
    let ret: boolean = false;
    let auth: AppUserAuth = null;

    // Retrieve security object
    auth = this.securityObject;
    if (auth) {
      // See if the claim type has a value
      // *hasClaim="'claimType:value'"
      if (claimType.indexOf(":") >= 0) {
        let words: string[] = claimType.split(":");
        claimType = words[0].toLowerCase();
        claimValue = words[1];
      }
      else {
        claimType = claimType.toLowerCase();
        // Either get the claim value, or assume 'true'
        claimValue = claimValue ? claimValue : "true";
      }
      // Attempt to find the claim
      ret = auth.claims.find(c =>
        c.claimType.toLowerCase() == claimType &&
        c.claimValue == claimValue) != null;
    }

    return ret;
  }
}

