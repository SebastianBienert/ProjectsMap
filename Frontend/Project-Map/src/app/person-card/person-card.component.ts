import { Component, OnInit, Input } from '@angular/core';
import { Employee } from '../common-interfaces/employee';
import { Router } from '@angular/router';

@Component({
  selector: 'app-person-card',
  templateUrl: './person-card.component.html',
  styleUrls: ['./person-card.component.css']
})
export class PersonCardComponent implements OnInit {

  @Input() employee : Employee;
  constructor(public router: Router) { }

  ngOnInit() {
  }

  showMore(){
    this.router.navigate(['/main',{outlets: {right: ['user', this.employee.Id], center: [this.employee.Id]} }]);
  }

}
