import {Component, OnInit} from '@angular/core';
import {NgOptimizedImage} from "@angular/common";
import {Router, RouterLink} from "@angular/router";
import { NgbTooltipModule } from '@ng-bootstrap/ng-bootstrap';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {FormsModule} from "@angular/forms";

@Component({
  selector: 'app-registracija',
  standalone: true,
  imports: [
    NgOptimizedImage,
    RouterLink,
    NgbTooltipModule,
    HttpClientModule,
    FormsModule
  ],
  templateUrl: './registracija.component.html',
  styleUrl: './registracija.component.css',
  host: { class: 'd-block' }
})
export class RegistracijaComponent implements OnInit{

  ngIme:any;
  ngPrezime:any;
  ngKorisnickoIme:any;
  ngEmail:any;
  ngDatumRodjenja:any;
  ngLozinka:any;
  constructor(public httpClient:HttpClient,private router:Router) {
  }
  ngOnInit(): void {
  }
  kreirajKorisnika() {
    let url= MojConfig.adresa_servera + `/Dodaj`;
    let requestBody = {
      "ime": this.ngIme,
      "prezime": this.ngPrezime,
      "korisnickoIme": this.ngKorisnickoIme,
      "email": this.ngEmail,
      "datumRodjenja": this.ngDatumRodjenja,
      "lozinka": this.ngLozinka
    }
    this.httpClient.post(url,requestBody).subscribe(x=>{
        this.router.navigate(["/"]);
    })
  }
}
