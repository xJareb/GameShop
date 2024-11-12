import {Component, EventEmitter, Input, input, Output} from '@angular/core';
import {NgbRatingModule} from "@ng-bootstrap/ng-bootstrap";
import {FormsModule} from "@angular/forms";
import {BrowserModule} from "@angular/platform-browser";
import {HttpClient} from "@angular/common/http";
import {MyAuthServiceService} from "../../../Servis/AuthService/my-auth-service.service";
import {RecenzijaRequest} from "../../../Servis/RecenzijeService/recenzija-request";
import {MojConfig} from "../../../moj-config";

@Component({
  selector: 'app-recenzija',
  standalone: true,
  imports: [NgbRatingModule, FormsModule],
  templateUrl: './recenzija.component.html',
  styleUrl: './recenzija.component.css'
})
export class RecenzijaComponent {
  rating = 1;
  public prikaz:boolean = true;
  @Input() igricaID:any;
  @Output() otvori = new EventEmitter<boolean>();
  public recenzijaRequest:RecenzijaRequest | null = null;

  constructor(public httpClient:HttpClient, public authService:MyAuthServiceService) {
  }

  ostaviRecenziju() {
    let sadrzaj = document.getElementById('message-text') as HTMLInputElement;
    let korisnikID = this.authService.dohvatiAutorzacijskiToken()?.autentifikacijaToken.korisnikID;
    let url = MojConfig.adresa_servera + `/DodajRecenziju`

    this.recenzijaRequest = {
      korisnikID:korisnikID,
      igricaID:this.igricaID,
      sadrzaj:sadrzaj.value,
      ocjena:this.rating
    }
    if(this.recenzijaRequest.sadrzaj != ""){

    }
    this.httpClient.post(url,this.recenzijaRequest).subscribe(x=>{
      this.zatvori();
      window.location.reload();
    })
  }

  zatvori() {
    this.prikaz = !this.prikaz;
    this.otvori.emit(this.prikaz);
  }
}
