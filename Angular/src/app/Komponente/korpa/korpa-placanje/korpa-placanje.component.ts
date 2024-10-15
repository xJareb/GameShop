import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {NgForOf, NgIf} from "@angular/common";
import {ReactiveFormsModule} from "@angular/forms";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-korpa-placanje',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './korpa-placanje.component.html',
  styleUrl: './korpa-placanje.component.css'
})
export class KorpaPlacanjeComponent implements OnInit{

    @Input() ukupno:number = 0;
    @Output() otvori = new EventEmitter<boolean>();
    private prikaz: boolean = true;

    constructor() {
    }
    ngOnInit(): void {

    }
  zatvori() {
    this.prikaz = !this.prikaz;
    this.otvori.emit(this.prikaz);
  }
}
