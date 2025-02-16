import {Component, OnInit} from '@angular/core';
import {NgbPopoverModule} from "@ng-bootstrap/ng-bootstrap";
import {NgForOf, NgIf} from "@angular/common";
import {MojConfig} from "../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {Router, RouterLink} from "@angular/router";
import {SedmicnaPonudaComponent} from "../special-offer/sedmicna-ponuda.component";
import {withNoHttpTransferCache} from "@angular/platform-browser";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {routes} from "../../app.routes";
import {RecenzijaComponent} from "../game-details/recenzija/recenzija.component";
import {RecenzijeComponent} from "../reviews/recenzije.component";
import {IzdvojenaIgricaComponent} from "../highlighted-game/izdvojena-igrica.component";
import {KontaktPodnozjeComponent} from "../contact-footer/kontakt-podnozje.component";
import {AllGamesResponse} from "../../Servis/IgriceService/all-games-response";
import {GameCategoriesResponse} from "../../Servis/IgriceService/all-games-category-response";

declare const google: any;

@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [NgbPopoverModule, NgIf, HttpClientModule, NgForOf, SedmicnaPonudaComponent, RouterLink, RecenzijaComponent, RecenzijeComponent, IzdvojenaIgricaComponent, KontaktPodnozjeComponent],
  templateUrl: './pocetna.component.html',
  styleUrl: './pocetna.component.css',
  host: { class: 'd-block' },
})
export class PocetnaComponent implements OnInit{

  gameList:any;
  statusModal: boolean = false;

  constructor(public httpClient:HttpClient,private router:Router, public authService:MyAuthServiceService) {
  }
  loadGames($event: Event) {
    // @ts-ignore
    let name = $event.target.value;
    let url = MojConfig.adresa_servera + `/GetByCategory?Sorting=asc&GameName=${name}`;

    if(name != ""){
      this.statusModal = true;
      this.httpClient.get<GameCategoriesResponse>(url).subscribe(x=>{
        this.gameList=x.games;
      })
    }
    else{
      this.statusModal = false;
    }
  }
  name:any;
  ngOnInit(): void {
    this.name = this.authService.showName();
  }

  goToRoute(li: any) {
    let igricaID = li.id;
    this.router.navigate([`/game-details/${igricaID}`])
  }

  logout() {
    let token = window.localStorage.getItem("my-auth-token")??"";
    let korisnik = window.localStorage.getItem("korisnik")??"";
    let ime = window.localStorage.getItem("ime");


    window.localStorage.setItem("korisnik","");

    let url = MojConfig.adresa_servera + `/Logout`;

    this.httpClient.post(url,{},{
      headers: {
        "my-auth-token":token
      }
    }).subscribe(x=>{

      window.localStorage.removeItem("my-auth-token");
    })
  }
  goToPage() {
    if(!this.authService.isLogged()){
      this.router.navigate(["/login"])
    }
    if(this.authService.isLogged() && this.authService.isAdmin()){
      this.router.navigate(["/admin"])
    }
    if(this.authService.isLogged() && this.authService.isUser()){
      this.router.navigate(["/korisnik"])
    }
  }
}
