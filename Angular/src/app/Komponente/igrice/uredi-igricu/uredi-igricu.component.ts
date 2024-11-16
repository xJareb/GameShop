import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import { FormsModule } from '@angular/forms';
import {MojConfig} from "../../../moj-config";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {NgForOf} from "@angular/common";
import {MyAuthServiceService} from "../../../Servis/AuthService/my-auth-service.service";
import {GenreResponse} from "../../../Servis/ZanrService/genre-response";
import {GameUpdateRequest} from "../../../Servis/IgriceService/game-update-request";


@Component({
  selector: 'app-uredi-igricu',
  standalone: true,
  imports: [FormsModule, HttpClientModule, NgForOf],
  templateUrl: './uredi-igricu.component.html',
  styleUrl: './uredi-igricu.component.css'
})
export class UrediIgricuComponent implements OnInit{

  @Input() gameID!:number;
  @Input() name!:string;
  @Input() genreID!:number;
  @Input() releaseDate!:Date;
  @Input() photo!:string;
  @Input() publisher!:string;
  @Input() description!:string;
  @Input() price!:number;
  @Input() percentageDiscount!:number;
  @Output() open = new EventEmitter<boolean>();
  show:boolean = true;

  genreList:any;
  public selectedGame:GameUpdateRequest | null = null;

  ngOnInit(): void {
    this.listGenres();

  }
  constructor(public httpClient:HttpClient, public authservice:MyAuthServiceService) {
  }
  listGenres(){
    let url = MojConfig.adresa_servera + `/GenresGet`;

    this.httpClient.get<GenreResponse>(url).subscribe(x=>{
        this.genreList = x.genres;
    })
  }

  saveChanges() {
    let url = MojConfig.adresa_servera + `/GameUpdate`;

    this.selectedGame = {
      gameID: this.gameID,
      name: this.name,
      genreID: this.genreID,
      releaseDate: this.releaseDate,
      photo: this.photo,
      publisher: this.publisher,
      description: this.description,
      price: this.price,
      percentageDiscount: this.percentageDiscount
    }

    this.httpClient.put(url,this.selectedGame,{
      headers:{
        "my-auth-token": this.authservice.vratiToken()
      }
    }).subscribe(x=>{
      this.close();
      window.location.reload();
    })
  }

  close() {
    this.show = !this.show;
    this.open.emit(this.show);
  }
}
