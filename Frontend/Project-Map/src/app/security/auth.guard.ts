import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { SecurityService } from './security.service';

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private securityService: SecurityService,
    private router: Router) { }

  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> | Promise<boolean> | boolean {
    // Get property name on security object to check
    let claimType: string = next.data["claimType"];

    let secObject = localStorage.getItem("bearerToken");
    if(secObject !== null) {
      this.securityService.mapResponse(secObject);
    }
    
    this.securityService.setTimer();
    this.securityService.timer.subscribe(x => {
      this.securityService.logout();
      this.router.navigate(['login'], { queryParams: { returnUrl: state.url } });
    });

    if (this.securityService.securityObject.isAuthenticated
      && this.securityService.hasClaim(claimType) && this.securityService.isUserStillValid()) {
      return true;
    }
    else {
      if(state.url === null || state.url.length == 0) {
        this.router.navigate(['login'],
        { queryParams: { returnUrl: "//main" } });
      }
      else {
        this.router.navigate(['login'],
        { queryParams: { returnUrl: state.url } });
      }
      return false;
    }
  }
}
