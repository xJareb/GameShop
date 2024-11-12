import {Component, OnInit} from '@angular/core';
import {NgbPopoverModule} from "@ng-bootstrap/ng-bootstrap";
import {NgForOf, NgIf} from "@angular/common";
import {MojConfig} from "../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ListaIgrica} from "../../Servis/IgriceService/lista-igrica";
import {Router, RouterLink} from "@angular/router";
import {SedmicnaPonudaComponent} from "../sedmicna-ponuda/sedmicna-ponuda.component";
import {withNoHttpTransferCache} from "@angular/platform-browser";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {routes} from "../../app.routes";
import {RecenzijaComponent} from "../detalji-igrice/recenzija/recenzija.component";
import {RecenzijeComponent} from "../recenzije/recenzije.component";
import {IzdvojenaIgricaComponent} from "../izdvojena-igrica/izdvojena-igrica.component";
import {KontaktPodnozjeComponent} from "../kontakt-podnozje/kontakt-podnozje.component";

declare const google: any;

@Component({
  selector: 'app-pocetna',
  standalone: true,
  imports: [NgbPopoverModule, NgIf, HttpClientModule, NgForOf, SedmicnaPonudaComponent, RouterLink, RecenzijaComponent, RecenzijeComponent, IzdvojenaIgricaComponent, KontaktPodnozjeComponent],
  templateUrl: './pocetna.component.html',
  styleUrl: './pocetna.component.css',
  host: { class: 'd-block' },
})
export class PocetnaComponent implements OnInit{

  protected readonly console = console;
  protected readonly event = event;

  listaIgrica:any;
  statusModal: boolean = false;

  constructor(public httpClient:HttpClient,private router:Router, public authService:MyAuthServiceService) {
  }
  ucitajIgrice($event: Event) {
    // @ts-ignore
    let naziv = $event.target.value;
    let url = MojConfig.adresa_servera + `/ByKategorija?Sortiranje=asc&NazivIgrice=${naziv}`;

    if(naziv != ""){
      this.statusModal = true;
      this.httpClient.get<ListaIgrica>(url).subscribe(x=>{
        this.listaIgrica=x.igrice;
      })
    }
    else{
      this.statusModal = false;
    }
  }
  ime:any;
  ngOnInit(): void {
    this.ime = this.authService.prikazImena();
  }

  idiNaRutu(li: any) {
    let igricaID = li.id;
    this.router.navigate([`/detalji-igrice/${igricaID}`])
  }

  odjaviSe() {
    let token = window.localStorage.getItem("my-auth-token")??"";
    let korisnik = window.localStorage.getItem("korisnik")??"";
    let ime = window.localStorage.getItem("ime");

    window.localStorage.setItem("korisnik","");


    let url = MojConfig.adresa_servera + `/Odjavi-se`;

    this.httpClient.post(url,{},{
      headers: {
        "my-auth-token":token
      }
    }).subscribe(x=>{
      window.localStorage.removeItem("my-auth-token");
      window.location.reload();
    })
  }
  idiNaStranicu() {
    if(!this.authService.jelLogiran()){
      this.router.navigate(["/prijava"])
    }
    if(this.authService.jelLogiran() && this.authService.jelAdmin()){
      this.router.navigate(["/admin"])
    }
    if(this.authService.jelLogiran() && this.authService.jelKorisnik()){
      this.router.navigate(["/korisnik"])
    }
  }
}
