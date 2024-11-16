import {Component, OnInit} from '@angular/core';
import {FormsModule} from "@angular/forms";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {NgForOf, NgIf} from "@angular/common";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {AllGamesResponse, Game} from "../../Servis/IgriceService/all-games-response";

@Component({
  selector: 'app-izdvojena-igrica',
  standalone: true,
  imports: [
    FormsModule, HttpClientModule, NgForOf, NgIf
  ],
  templateUrl: './izdvojena-igrica.component.html',
  styleUrl: './izdvojena-igrica.component.css'
})
export class IzdvojenaIgricaComponent implements OnInit{
    public gameList:Game[]=[];
    public odabranaIgrica: Game[] = [];

    public ngGameID:number = 5;
    constructor(public httpClient:HttpClient, public authService:MyAuthServiceService) {
    }
    ngOnInit(): void {
        this.listAllGames();
        this.listCheckedGame();
    }
    listAllGames(){
      let url = MojConfig.adresa_servera + `/GamesGet`;

      this.httpClient.get<AllGamesResponse>(url).subscribe(x=>{
        this.gameList = x.game;
      })
    }
    listCheckedGame(){
      let url = MojConfig.adresa_servera + `/GamesGet?GameID=${this.ngGameID}`;

      this.httpClient.get<AllGamesResponse>(url).subscribe(x=>{
        this.odabranaIgrica = x.game;
      })
    }

}
