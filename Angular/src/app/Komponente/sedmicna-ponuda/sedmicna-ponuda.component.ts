import {Component, OnInit} from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";
import {MojConfig} from "../../moj-config";
import {HttpClient} from "@angular/common/http";
import {SedmicnaPonuda} from "../../Servis/SedmicnaPonudaService/sedmicna-ponuda";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-sedmicna-ponuda',
  standalone: true,
  imports: [
    NgForOf,
    NgIf
  ],
  templateUrl: './sedmicna-ponuda.component.html',
  styleUrl: './sedmicna-ponuda.component.css'
})
export class SedmicnaPonudaComponent implements OnInit{

    sedmicnaPonuda:any;

    ngOnInit(): void {
      this.izlistajPonudu();
    }
    constructor(public httpClient:HttpClient, public authService:MyAuthServiceService, private router:Router) {
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
      this.izlistajPonudu();
    })
  }

  idiUdetalje(sp: any) {
    let igricaID = sp.id;
    this.router.navigate([`/detalji-igrice/${igricaID}`])
  }
}
