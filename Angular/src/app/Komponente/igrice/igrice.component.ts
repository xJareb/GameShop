import {Component, OnInit} from '@angular/core';
import {Router, RouterLink} from "@angular/router";
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {NgClass, NgForOf, NgIf} from "@angular/common";
import {FormsModule} from "@angular/forms";
import {filter} from "rxjs";
import {UrediIgricuComponent} from "./uredi-igricu/uredi-igricu.component";
import {DodajIgricuComponent} from "./dodaj-igricu/dodaj-igricu.component";
import {MyAuthServiceService} from "../../Servis/AuthService/my-auth-service.service";
import {AllGamesResponse} from "../../Servis/IgriceService/all-games-response";
import {GenreResponse} from "../../Servis/ZanrService/genre-response";
import {GameCategoriesResponse} from "../../Servis/IgriceService/all-games-category-response";
import {GameUpdateRequest} from "../../Servis/IgriceService/game-update-request";

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

  public selectedGame:GameUpdateRequest | null = null;
  urediModal:boolean = false;

  gameList:any;
  genreList:any;

  firstPrice:number = 1;
  lastPrice:number = 100;
  genre:any;
  sort:any;
  showModalEdit:boolean = false;
  showModalAdd: boolean = false;
  constructor(public httpClient:HttpClient,private router:Router,public authService:MyAuthServiceService) {
  }
  ngOnInit(): void {
    this.filter(0);
    this.listGenres();
  }

  goToDetails(li: any) {
    let gameID = li.id;
    this.router.navigate([`/detalji-igrice/${gameID}`])
  }
  listGenres(){
    let url = MojConfig.adresa_servera + `/GenresGet`;
    this.httpClient.get<GenreResponse>(url).subscribe(x=>{
      this.genreList = x.genres;
    })
  }

  filter(genreID:any) {
    this.genre = genreID
    let url = MojConfig.adresa_servera +
      `/GetByCategory?GenreID=${genreID}&FirstPrice=${1}&LastPrice=${200}`

    this.httpClient.get<GameCategoriesResponse>(url).subscribe((x:GameCategoriesResponse)=>{
      this.gameList = x.games;
    })

  }
  handleGenre(lz: any) {
    this.genre = lz.id;
  }

  test() {
    let select = document.getElementById('sort') as HTMLSelectElement;
    this.sort  = select?.value;

  }
  filter2(genreID:any,pocetnacijena:any,zavrsnacijena:any,sorting:any){
    this.genre = genreID
    this.sort = sorting

    let url = MojConfig.adresa_servera +
      `/GetByCategory?GenreID=${this.genre}&FirstPrice=${this.firstPrice}&LastPrice=${this.lastPrice}&Sorting=${sorting}`

    this.httpClient.get<GameCategoriesResponse>(url).subscribe(x=>{
      this.gameList = x.games;
    })
  }

    deleteGame(li: any) {
        let gameID = li.id;
        let url = MojConfig.adresa_servera + `/GameDelete?GameID=${gameID}`;

        this.httpClient.delete(url,{
          headers:{
            "my-auth-token":this.authService.vratiToken()
          }
        }).subscribe(x=>{
          window.location.reload();
        })
    }

  highlightGame(li: any) {
      let igricaID = li.id;
      let url = MojConfig.adresa_servera + `/IzdvojiIgricu?IgricaID=${igricaID}&Izdvojeno=true`;

      this.httpClient.put(url,{},{
        headers:{
          "my-auth-token": this.authService.vratiToken()
        }
      }).subscribe(x=>{
          window.location.reload();
      })
  }

  prepareData(li: any) {
    this.selectedGame = {
      gameID: li.id,
      name: li.name,
      genreID: li.genreID,
      releaseDate: li.releaseDate,
      photo: li.photo,
      publisher: li.publisher,
      description: li.description,
      price: li.price,
      percentageDiscount: li.percentageDiscount
    }
    console.log(li.releaseDate)
  }
  openModalEdit($event : boolean)
  {
    this.showModalEdit = $event;
  }
  openModalAdd($event : boolean)
  {
    this.showModalAdd = $event;
  }
}
