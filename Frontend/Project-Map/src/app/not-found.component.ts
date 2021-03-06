import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  template: '<h2><span style="font-style: italic">{{router.url.substr(1)}}</span> was not found</h2>'
})
export class PageNotFoundComponent {
	constructor(public router: Router) { }
}