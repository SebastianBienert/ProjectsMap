import { AppUserClaim } from "./app-user-claim";

export class AppUserAuth {
  userName: string = "";
  access_token: string = "";
  isAuthenticated: boolean = false;
  claims: AppUserClaim[] = [];
  userId: string = "";
}
