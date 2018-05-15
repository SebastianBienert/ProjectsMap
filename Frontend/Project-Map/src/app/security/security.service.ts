import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { of } from 'rxjs/observable/of';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { tap } from 'rxjs/operators/tap';
import { Globals } from './../globals';
import { AppUserAuth } from './app-user-auth';
import { AppUser } from './app-user';
import * as JWT from 'jwt-decode';
import { AppUserClaim } from './app-user-claim';
import { HttpErrorHandler, HandleError } from '../services/http-error-handler.service';
import { catchError } from 'rxjs/operators';


const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/x-www-form-urlencoded',

  })
};


@Injectable()
export class SecurityService {
  securityObject: AppUserAuth = new AppUserAuth();
  Api_Url: string;
  private handleError: HandleError;

  constructor(private http: HttpClient,
  httpErrorHandler: HttpErrorHandler,
  private globals: Globals) {
    this.Api_Url = globals.getUrl() + "/oauth/";
    
    this.handleError = httpErrorHandler.createHandleError('ProjectService');
   
  }



   register(entity: any): Observable<any>{
     return this.http.post<any>(this.globals.getUrl() + "/api/accounts/create", entity)
     .pipe(
      catchError(this.handleError('createAccount', []))
    );
   }

  initializeSecurityObj 

  isUserStillValid(): boolean{
    let claim: AppUserClaim = this.securityObject.claims.find(x => x.claimType == "exp");
    let expires = +claim.claimValue;
    let current = Date.now() / 1000;
    if(current <= expires) {
       return true;
    }
    else {
       this.logout();
    }
    
    return false;
  }

  login(entity: AppUser): Observable<AppUserAuth> {
    // Initialize security object
    this.resetSecurityObject();

    let body = new URLSearchParams();
    body.set('username', entity.userName);
    body.set('password', entity.password);
    body.set('grant_type', 'password');

    return this.http.post<AppUserAuth>(this.Api_Url + "token",
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

    if (typeof claimType === "string") {
      ret = this.isClaimValid(claimType, claimValue);
    }
    else {
      let claims: string[] = claimType;
      if (claims) {
        for (let index = 0; index < claims.length; index++) {
          ret = this.isClaimValid(claims[index]);
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

    auth = this.securityObject;
    if (auth) {
      if (claimType.indexOf(":") >= 0) {
        let words: string[] = claimType.split(":");
        claimType = words[0].toLowerCase();
        claimValue = words[1];
      }
      else {
        claimType = claimType.toLowerCase();
        claimValue = claimValue ? claimValue : "true";
      }
      ret = auth.claims.find(c =>
        c.claimType.toLowerCase() == claimType &&
        c.claimValue == claimValue) != null;
    }

    return ret;
  }
}

