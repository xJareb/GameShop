import { Injectable } from '@angular/core';
import {Router} from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class MyAuthServiceService {

  constructor(public router:Router) { }

  handleAuthToken():any | null{
    let tokenString = window.localStorage.getItem("korisnik")??"";
    try {
      return JSON.parse(tokenString);
    }
    catch (e){
      return null;
    }
  }
  userID():number{
    return this.handleAuthToken()?.authenticationToken.userID ?? 0;
  }
  isAdmin():boolean{
    return this.handleAuthToken()?.authenticationToken.userAccount.isAdmin ?? false;
  }
  isLogged():boolean{
    return this.handleAuthToken()?.isLogged ?? false;
  }
  showName():string {
    return this.handleAuthToken()?.authenticationToken.userAccount.username ?? "Login";
  }
  isUser():boolean{
    return this.handleAuthToken()?.authenticationToken.userAccount.isUser ?? false;
  }
  isGoogleProvider():boolean{
    return this.handleAuthToken()?.authenticationToken.userAccount.isGoogleProvider ?? false;
  }
  returnToken():string{
    return this.handleAuthToken()?.authenticationToken.value;
  }
  disableShoppingCart(){
    if(!this.isLogged()){
      this.router.navigate(["/login"]);
    }
  }
}
