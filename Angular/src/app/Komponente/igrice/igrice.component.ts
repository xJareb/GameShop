import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {Igrice, ListaIgrica} from "../../Servis/IgriceService/lista-igrica";
import {NgClass, NgForOf, NgIf} from "@angular/common";
import {Zanr} from "../../Servis/ZanrService/zanr";
import {FormsModule} from "@angular/forms";
import {filter} from "rxjs";
import {UrediIgricuComponent} from "./uredi-igricu/uredi-igricu.component";
import {UrediIgricu} from "../../Servis/IgriceService/uredi-igricu";
import {DodajIgricuComponent} from "./dodaj-igricu/dodaj-igricu.component";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";

@Component({
  selector: 'app-igrice',
  standalone: true,
  imports: [
    RouterLink,
    HttpClientModule,
    NgForOf,
    NgIf,
    FormsModule,
    UrediIgricuComponent,
    DodajIgricuComponent,
    NgClass,
  ],
  templateUrl: './igrice.component.html',
  styleUrl: './igrice.component.css'
})
export class IgriceComponent implements OnInit{

  public odabranaIgrica:UrediIgricu | null = null;
  urediModal:boolean = false;

  listaIgrica:any;
  listaZanrova:any;

  pocetnaCijena:number = 1;
  zavrsnaCijena:number = 100;
  zanr:any;
  sort:any;
  prikazUredi:boolean = false;
  prikazDodaj: boolean = false;
  constructor(public httpClient:HttpClient,private router:Router,public authService:MyAuthServiceService) {
  }
  ngOnInit(): void {
    this.filter(0,1,250);
    this.izlistajZanrove();
  }

  idiUDetalje(li: any) {
    let igricaID = li.id;
    this.router.navigate([`/detalji-igrice/${igricaID}`])
  }
  izlistajZanrove(){
    let url = MojConfig.adresa_servera + `/Izlistaj`;
    this.httpClient.get<Zanr>(url).subscribe(x=>{
      this.listaZanrova = x.zanrovi;
    })
  }

  filter(zanrid:any,pocetnacijena:any,zavrsnacijena:any) {
    this.zanr = zanrid
    let url = MojConfig.adresa_servera +
      `/ByKategorija?ZanrID=${this.zanr}&PocetnaCijena=${this.pocetnaCijena}&KrajnjaCijena=${this.zavrsnaCijena}`

    this.httpClient.get<ListaIgrica>(url).subscribe(x=>{
      this.listaIgrica = x.igrice;
    })

  }
  uzmiZanr(lz: any) {
    this.zanr = lz.id;
  }

  test() {
    let select = document.getElementById('sort') as HTMLSelectElement;
    this.sort  = select?.value;

  }
  filter2(zanrid:any,pocetnacijena:any,zavrsnacijena:any,sortiranje:any){
    this.zanr = zanrid
    this.sort = sortiranje

    let url = MojConfig.adresa_servera +
      `/ByKategorija?ZanrID=${this.zanr}&PocetnaCijena=${this.pocetnaCijena}&KrajnjaCijena=${this.zavrsnaCijena}&Sortiranje=${this.sort}`

    this.httpClient.get<ListaIgrica>(url).subscribe(x=>{
      this.listaIgrica = x.igrice;
    })
  }

    obirisiIgricu(li: any) {
        let igricaID = li.id;
        let url = MojConfig.adresa_servera + `/ObrisiIgricu?IgricaID=${igricaID}`;

        this.httpClient.delete(url).subscribe(x=>{
          window.location.reload();
        })
    }

  izdvojiIgricu(li: any) {
      let igricaID = li.id;
      let url = MojConfig.adresa_servera + `/IzdvojiIgricu?IgricaID=${igricaID}&Izdvojeno=true`;

      this.httpClient.put(url,{}).subscribe(x=>{
          window.location.reload();
      })
  }

  pripremiPodatke(li: any) {
    this.odabranaIgrica = {
      igricaID: li.id,
      naziv: li.naziv,
      zanrID: li.zanrID,
      datumIzlaska: li.datumIzlaska,
      slika: li.slika,
      izdavac: li.izdavac,
      opis: li.opis,
      cijena: li.cijena,
      postotakAkcije: li.postotakAkcije
    }
  }
  otvaranjeUredi($event : boolean)
  {
    this.prikazUredi = $event;
  }
  otvaranjeDodaj($event : boolean)
  {
    this.prikazDodaj = $event;
  }
}
