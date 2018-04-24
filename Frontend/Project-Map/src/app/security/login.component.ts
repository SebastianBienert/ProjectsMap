import { Component, OnInit } from '@angular/core';
import { AppUser } from './app-user';
import { AppUserAuth } from './app-user-auth';
import { SecurityService } from './security.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'ptc-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: AppUser = new AppUser();
  securityObject: AppUserAuth = null;
  returnUrl: string;

  constructor(private securityService: SecurityService,
    private route: ActivatedRoute,
    private router: Router) {
      document.body.style.backgroundImage = "url('../../assets/background.jpg')";
      document.body.style.backgroundPosition = "center center";
      document.body.style.backgroundRepeat = "no-repeat";
      document.body.style.backgroundAttachment = "fixed";
      document.body.style.backgroundSize = "cover";
     }

  ngOnInit() {
    this.returnUrl = this.route.snapshot.queryParamMap.get('returnUrl');
  }

  ngOnDestroy(){
    document.body.style.backgroundImage = "none";
  }

  login() {
    this.securityService.login(this.user)
      .subscribe(resp => {
        this.securityObject = resp;
        if (this.returnUrl) {
          console.log("weszlem");
          this.router.navigateByUrl(this.returnUrl);
        }
      },
        () => {
          // Initialize security object to display error message
          this.securityObject = new AppUserAuth();
        });
  }
}