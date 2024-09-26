import {Component, OnInit} from '@angular/core';
import {NgbPopoverModule} from "@ng-bootstrap/ng-bootstrap";
import {NgForOf, NgIf} from "@angular/common";
import {MojConfig} from "../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ListaIgrica} from "../igrice/lista-igrica";
import {Router, RouterLink} from "@angular/router";
import {SedmicnaPonudaComponent} from "../sedmicna-ponuda/sedmicna-ponuda.component";
import {withNoHttpTransferCache} from "@angular/platform-browser";

@Component({
  selector: 'app-pocetna',
  standalone: true,
  imports: [NgbPopoverModule, NgIf, HttpClientModule, NgForOf, SedmicnaPonudaComponent, RouterLink],
  templateUrl: './pocetna.component.html',
  styleUrl: './pocetna.component.css',
  host: { class: 'd-block' },
})
export class PocetnaComponent implements OnInit{

  protected readonly console = console;
  protected readonly event = event;

  listaIgrica:any;
  statusModal: boolean = false;

  constructor(public httpClient:HttpClient,private router:Router) {
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
    this.ime = window.localStorage.getItem("ime");

    console.log(this.ime)
  }

  idiNaRutu(li: any) {
    let igricaID = li.id;
    this.router.navigate([`/detalji-igrice/${igricaID}`])
  }

  odjaviSe() {
    let token = window.localStorage.getItem("my-auth-token")??"";
    let korisnik = window.localStorage.getItem("korisnik")??"";
    let ime = window.localStorage.getItem("ime");

    window.localStorage.setItem("my-auth-token","");
    window.localStorage.setItem("korisnik","");
    window.localStorage.setItem("ime","Prijavi se")


    let url = MojConfig.adresa_servera + `/Odjavi-se`;

    this.httpClient.post(url,{},{
      headers: {
        "my-auth-token":token
      }
    }).subscribe(x=>{
      alert('Uspješno odjavljen');
      window.location.reload();
    })
  }
}
