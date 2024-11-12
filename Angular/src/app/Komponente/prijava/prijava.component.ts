import {AfterViewInit, Component, OnInit} from '@angular/core';
import {FormControl, FormGroup, FormsModule, ReactiveFormsModule, Validators} from "@angular/forms";
import {Route, Router, RouterLink} from "@angular/router";
import {MojConfig} from "../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {PrijavaRequest} from "../../Servis/PrijavaService/prijava-request";
import {PrijavaResponse} from "../../Servis/PrijavaService/prijava-response";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {NgIf} from "@angular/common";

declare const google: any;

@Component({
  selector: 'app-prijava',
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

  public loginPodaci: PrijavaRequest|null = null;
  lozinka: any;
  korisnickoIme: any;


  constructor(public httpClient:HttpClient, private route:Router, public authService:MyAuthServiceService) {
  }

  ngOnInit(): void {
        if(this.authService.jelLogiran()){
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
      "ime": payload.name,
      "email": payload.email,
      "slika": payload.picture,
      "isGoogleProvider": true
    }

    let url = MojConfig.adresa_servera + `/DodajGoogleKorisnik`

    this.httpClient.post(url,requestBody).subscribe(x=>{
    })
    this.googlePrijava(payload.email);
  }
  googlePrijava(email:string){
    let url = MojConfig.adresa_servera + `/Prijavi-se-google?Email=${email}`;

    this.httpClient.post<PrijavaResponse>(url,{}).subscribe(x=>{
      if(!x.isLogiran || x.autentifikacijaToken.korisnickiNalog.isDeleted || x.autentifikacijaToken.korisnickiNalog.isBlackList){
        alert('Greška na serveru');
        return;
      }else{
        window.localStorage.setItem('my-auth-token', x.autentifikacijaToken.vrijednost);
        window.localStorage.setItem('korisnik', JSON.stringify(x));
        this.route.navigate(["/"]);
      }
    })
  }

  prijaviSe() {
    let url = MojConfig.adresa_servera + `/Prijavi-se`;

    this.loginPodaci = {
      korisnickoIme: this.korisnickoIme,
      lozinka: this.lozinka
    };

    if(this.loginPodaci.korisnickoIme != null && this.loginPodaci.lozinka != null){
    this.httpClient.post<PrijavaResponse>(url, this.loginPodaci).subscribe(x => {
        if (!x.isLogiran || x.autentifikacijaToken.korisnickiNalog.isDeleted || x.autentifikacijaToken.korisnickiNalog.isBlackList) {
            this.postaviStil();
        }
        else {
          window.localStorage.setItem('my-auth-token', x.autentifikacijaToken.vrijednost);
          window.localStorage.setItem('korisnik', JSON.stringify(x));
          this.route.navigate(["/"]);
        }
      })
    }else{
      this.postaviStil();
    }
  }
  postaviStil(){
    let korisnickoImeInput = document.getElementById("exampleInputUsername1") as HTMLInputElement;
    let lozinkaInput = document.getElementById("exampleInputPassword1") as HTMLInputElement;

    korisnickoImeInput.style.backgroundColor = 'red';
    lozinkaInput.style.backgroundColor = 'red';
  }
}
