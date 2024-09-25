import {Component, OnInit} from '@angular/core';
import {NgForOf} from "@angular/common";
import {MojConfig} from "../../moj-config";
import {HttpClient} from "@angular/common/http";
import {SedmicnaPonuda} from "./sedmicna-ponuda";

@Component({
  selector: 'app-sedmicna-ponuda',
  standalone: true,
  imports: [
    NgForOf
  ],
  templateUrl: './sedmicna-ponuda.component.html',
  styleUrl: './sedmicna-ponuda.component.css'
})
export class SedmicnaPonudaComponent implements OnInit{

    sedmicnaPonuda:any;

    ngOnInit(): void {
      this.izlistajPonudu();
    }
    constructor(public httpClient:HttpClient) {
    }
    izlistajPonudu(){
      let url = MojConfig.adresa_servera + `/IzdvojeneIgrice`;

      this.httpClient.get<SedmicnaPonuda>(url).subscribe(x=>{
        this.sedmicnaPonuda = x.igrice;
      })
    }

  obirisIgricu(sp: any) {
    let igricaID = sp.id;
    let url = MojConfig.adresa_servera + `/IzdvojiIgricu?IgricaID=${igricaID}&Izdvojeno=false`;

    this.httpClient.put(url,{}).subscribe(x=>{
      alert('Uspješno uklonjena igrica iz sedmicne ponude');
      window.location.reload();
    })
  }
}
