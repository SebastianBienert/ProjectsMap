import { Component, OnInit, Input } from '@angular/core';
import { Employee } from '../common-interfaces/employee';

@Component({
  selector: 'app-person-card',
  templateUrl: './person-card.component.html',
  styleUrls: ['./person-card.component.css']
})
export class PersonCardComponent implements OnInit {

  @Input() employee : Employee;
  constructor() { }

  ngOnInit() {
  }

}
