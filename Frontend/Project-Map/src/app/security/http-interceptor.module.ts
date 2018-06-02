import { Injectable, NgModule } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpEvent, HttpInterceptor, HttpHandler,
         HttpRequest, 
         HttpResponse,
         HttpErrorResponse} from '@angular/common/http';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { SecurityService } from './security.service';
import { Router } from '@angular/router';

@Injectable()
export class HttpRequestInterceptor implements HttpInterceptor {

  constructor(private service : SecurityService,
    private router: Router){

  }

  intercept(req: HttpRequest<any>, next: HttpHandler):
    Observable<HttpEvent<any>> {
    var token = localStorage.getItem("bearerToken");

    if(token) {
      const newReq = req.clone(
        { 
           headers: req.headers.set('Authorization',
                    'Bearer ' + token)
        });

        return next.handle(newReq).do((event: HttpEvent<any>) => {
          if (event instanceof HttpResponse) {
            // do stuff with response if you want
          }
        }, (err: any) => {
          if (err instanceof HttpErrorResponse) {
            if (err.status === 401) {
              this.service.logout();
              this.router.navigate(['/login']);
            }
          }
        });
    }
    else {
      return next.handle(req).do((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          // do stuff with response if you want
        }
      }, (err: any) => {
        if (err instanceof HttpErrorResponse) {
          if (err.status === 401) {
            this.service.logout();
            this.router.navigate(['/login']);
          }
        }
      });
    }

  }
};

@NgModule({
  providers: [
    { provide: HTTP_INTERCEPTORS, 
      useClass: HttpRequestInterceptor, 
      multi: true }
  ]
})
export class HttpInterceptorModule { }