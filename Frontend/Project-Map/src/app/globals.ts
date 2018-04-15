import { Injectable } from '@angular/core';

@Injectable()
export class Globals {
  localhost: boolean = false;
  localhostUrl: string = "//localhost:58923";
  remoteUrl: string = "//projectsmapwebapi.azurewebsites.net";

  public getUrl() {
  	return this.localhost == false ? (this.remoteUrl):(this.localhostUrl);
  }
}