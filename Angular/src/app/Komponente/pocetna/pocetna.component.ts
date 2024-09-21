import {Component, OnInit} from '@angular/core';
import {NgbPopoverModule} from "@ng-bootstrap/ng-bootstrap";
import {NgForOf, NgIf} from "@angular/common";
import {MojConfig} from "../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {ListaIgrica} from "../igrice/lista-igrica";
import {Router} from "@angular/router";
import {SedmicnaPonudaComponent} from "../sedmicna-ponuda/sedmicna-ponuda.component";

@Component({
  selector: 'app-pocetna',
  standalone: true,
  imports: [NgbPopoverModule, NgIf, HttpClientModule, NgForOf, SedmicnaPonudaComponent],
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

  ngOnInit(): void {
  }

  idiNaRutu(li: any) {
    let igricaID = li.id;
    this.router.navigate([`/detalji-igrice/${igricaID}`])
  }
}
