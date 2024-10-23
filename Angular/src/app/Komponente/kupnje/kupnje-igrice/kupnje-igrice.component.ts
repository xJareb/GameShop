import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {HttpClient, HttpClientModule} from "@angular/common/http";
import {NgForOf} from "@angular/common";

@Component({
  selector: 'app-kupnje-igrice',
  standalone: true,
  imports: [
    HttpClientModule,
    NgForOf
  ],
  templateUrl: './kupnje-igrice.component.html',
  styleUrl: './kupnje-igrice.component.css'
})
export class KupnjeIgriceComponent implements OnInit{

  constructor(public httpClient:HttpClient) {
  }

  ngOnInit(): void {

  }
  @Input() listaIgrica:any;
  @Output() otvori = new EventEmitter<boolean>();
  private prikaz: boolean = true;

  zatvori() {
  this.prikaz = !this.prikaz;
  this.otvori.emit(this.prikaz);
  }

}
