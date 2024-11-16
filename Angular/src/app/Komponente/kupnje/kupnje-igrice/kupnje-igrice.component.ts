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
  @Input() gameList:any;
  @Output() open = new EventEmitter<boolean>();
  private show: boolean = true;

  close() {
  this.show = !this.show;
  this.open.emit(this.show);
  }

}
