import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {NgForOf} from "@angular/common";
import {MojConfig} from "../../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MyAuthServiceService} from "../../../Servis/AuthService/my-auth-service.service";
import {GenreResponse} from "../../../Servis/ZanrService/genre-response";
import {GameAddRequest} from "../../../Servis/IgriceService/game-add-request";

@Component({
  selector: 'app-dodaj-igricu',
  standalone: true,
  imports: [
    FormsModule,
    NgForOf,
    HttpClientModule
  ],
  templateUrl: './dodaj-igricu.component.html',
  styleUrl: './dodaj-igricu.component.css'
})
export class DodajIgricuComponent implements OnInit{

    public listGenre:any;
    public newGameRequest:GameAddRequest | null  = null;

    public name: any;
    public genreID: number = 1;
    public releaseDate:any;
    public photo:any;
    public publisher:any;
    public description:any;
    public price:any;
    public percentageDiscount:any;

    @Output() open = new EventEmitter<boolean>();
    public show:boolean = true;

    ngOnInit(): void {
      this.listAllGenre();
    }
    constructor(public httpClient:HttpClient, public authservice:MyAuthServiceService) {
    }

    listAllGenre(){
      let url = MojConfig.adresa_servera + `/GenresGet`;

      this.httpClient.get<GenreResponse>(url).subscribe(x=>{
        this.listGenre = x.genres;
      })
    }

  addGame() {
    this.newGameRequest = {
      name: this.name,
      genreID: this.genreID,
      releaseDate: this.releaseDate,
      photo: this.photo,
      publisher: this.publisher,
      description: this.description,
      price: this.price,
      percentageDiscount: this.percentageDiscount,
    }

    let url = MojConfig.adresa_servera + `/GameAdd`;

    if(!this.checkObject(this.newGameRequest)){
      this.httpClient.post(url,this.newGameRequest,{
        headers:{
          "my-auth-token": this.authservice.vratiToken()
        }
      }).subscribe(x=>{
        window.location.reload();
      })
    }else{
    }

  }

   checkObject(obj: any): boolean {
    return Object.values(obj).some(value =>
      value === null || value === undefined || value === ''
    );
  }


  close() {
    this.show = !this.show;
    this.open.emit(this.show);
  }
}
