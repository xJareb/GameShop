import {AfterViewInit, Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {Route, Router, RouterLink} from "@angular/router";
import {MojConfig} from "../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {NgIf} from "@angular/common";
import {LoginRequest} from "../../Servis/PrijavaService/login-request";
import {LoginResponse} from "../../Servis/PrijavaService/login-response";

declare const google: any;

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    HttpClientModule,
    FormsModule,
    NgIf
  ],
  templateUrl: './prijava.component.html',
  styleUrl: './prijava.component.css'
})
export class PrijavaComponent implements OnInit{

  clientId: string = '316824710872-dfm9p799fd6krd5p1furj9mt9s04h1ta.apps.googleusercontent.com';

  public dataLogin: LoginRequest|null = null;
  password: any;
  username: any;


  constructor(public httpClient:HttpClient, private route:Router, public authService:MyAuthServiceService) {
  }

  ngOnInit(): void {
        if(this.authService.isLogged()){
          this.route.navigate(["/"])
        }
    google.accounts.id.initialize({
      client_id: this.clientId,
      callback: this.handleCredentialResponse.bind(this),
    });

    google.accounts.id.renderButton(
      document.getElementById('google-signin-button'),
      { theme: 'outline', size: 'medium', type: 'icon' }
    );
    }
  handleCredentialResponse(response: any) {
    const credential = response.credential;
    const payload = JSON.parse(atob(credential.split('.')[1]));

    let requestBody = {
      "name": payload.name,
      "email": payload.email,
      "photo": payload.picture,
      "isGoogleProvider": true
    }

    let url = MojConfig.adresa_servera + `/UserGoogleAdd`

    this.httpClient.post(url,requestBody).subscribe(x=>{
    })
    this.googleLogin(payload.email);
  }
  googleLogin(email:string){
    let url = MojConfig.adresa_servera + `/Login-google?Email=${email}`;

    this.httpClient.post<LoginResponse>(url,{}).subscribe(x=>{
      if(!x.isLogged || x.authenticationToken.userAccount.isDeleted || x.authenticationToken.userAccount.isBlackList){
        alert('Error');
        return;
      }else{
        window.localStorage.setItem('my-auth-token', x.authenticationToken.value);
        window.localStorage.setItem('korisnik', JSON.stringify(x));
        this.route.navigate(["/"]);
      }
    })
  }

  login() {
    let url = MojConfig.adresa_servera + `/Login`;

    this.dataLogin = {
      username: this.username,
      password: this.password
    };

    if(this.dataLogin.username != null && this.dataLogin.password != null){
    this.httpClient.post<LoginResponse>(url, this.dataLogin).subscribe(x => {
        if (!x.isLogged || x.authenticationToken.userAccount.isDeleted || x.authenticationToken.userAccount.isBlackList) {
            this.setStyle();
        }
        else {
          window.localStorage.setItem('my-auth-token', x.authenticationToken.value);
          window.localStorage.setItem('korisnik', JSON.stringify(x));
          this.route.navigate(["/"]);
        }
      })
    }else{
      this.setStyle();
    }
  }
  setStyle(){
    let korisnickoImeInput = document.getElementById("exampleInputUsername1") as HTMLInputElement;
    let lozinkaInput = document.getElementById("exampleInputPassword1") as HTMLInputElement;

    korisnickoImeInput.style.backgroundColor = 'red';
    lozinkaInput.style.backgroundColor = 'red';
  }
}
