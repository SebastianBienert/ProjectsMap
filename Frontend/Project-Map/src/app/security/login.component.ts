import { Component, OnInit } from '@angular/core';
import { AppUser } from './app-user';
import { AppUserAuth } from './app-user-auth';
import { SecurityService } from './security.service';
import { ActivatedRoute, Router } from '@angular/router';
import {NgbdModalBasic} from '../modal/modal-basic.component';
import {NgbModal, ModalDismissReasons} from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'ptc-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user: AppUser = new AppUser();
  securityObject: AppUserAuth = null;
  returnUrl: string;
  modalReference : any;

  constructor(private securityService: SecurityService,
    private route: ActivatedRoute,
    private router: Router,
    private modalService: NgbModal) {
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
          this.modalReference.close();
          this.router.navigateByUrl(this.returnUrl);
        }
      },
        () => {
          // Initialize security object to display error message
          this.securityObject = new AppUserAuth();
          this.modalReference.close();
        });
  }

  open(content) {
    this.modalReference = this.modalService.open(content)
    this.modalReference.result.then((result) => {
      //this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
     // this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }
}