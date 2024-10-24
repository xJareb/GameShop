import {AfterViewInit, Component, OnInit} from '@angular/core';
import {FormsModule, ReactiveFormsModule} from "@angular/forms";
import {Route, Router, RouterLink} from "@angular/router";
import {MojConfig} from "../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {PrijavaRequest} from "../../Servis/PrijavaService/prijava-request";
import {PrijavaResponse} from "../../Servis/PrijavaService/prijava-response";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {NgIf} from "@angular/common";


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

  public loginPodaci: PrijavaRequest|null = null;
  lozinka: any;
  korisnickoIme: any;

  constructor(public httpClient:HttpClient, private route:Router, public authService:MyAuthServiceService) {
  }

  ngOnInit(): void {
        if(this.authService.jelLogiran()){
          this.route.navigate(["/"])
        }
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
            alert('Podaci nisu ispravni')
        }
        else {
          window.localStorage.setItem('my-auth-token', x.autentifikacijaToken.vrijednost);
          window.localStorage.setItem('korisnik', JSON.stringify(x));
          this.route.navigate(["/"]);
        }
      })
    }else{
      alert('Podaci nisu ispravni');
    }

  }
}
