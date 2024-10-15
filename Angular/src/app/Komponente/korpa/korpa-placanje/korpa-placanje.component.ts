import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {NgForOf, NgIf, NgStyle} from "@angular/common";
import {FormControl, FormGroup, ReactiveFormsModule, Validators} from "@angular/forms";
import {RouterLink} from "@angular/router";

@Component({
  selector: 'app-korpa-placanje',
  standalone: true,
  imports: [
    NgForOf,
    NgIf,
    ReactiveFormsModule,
    RouterLink,
    NgStyle
  ],
  templateUrl: './korpa-placanje.component.html',
  styleUrl: './korpa-placanje.component.css'
})
export class KorpaPlacanjeComponent implements OnInit{

    @Input() ukupno:number = 0;
    @Output() otvori = new EventEmitter<boolean>();
    private prikaz: boolean = true;

    userPayment : FormGroup;

    constructor() {
      this.userPayment = new FormGroup({
        brojKartice: new FormControl("",[Validators.required,Validators.pattern(/^\d{16}$/)]),
        imePrezime: new FormControl("",[Validators.required,Validators.pattern(/^[A-Z][a-z]+ [A-Z][a-z]+$/)]),
        datumIsteka: new FormControl("",[Validators.required]),
        sigurnosniBroj: new FormControl("",[Validators.required,Validators.pattern(/^\d{3}$/)]),
      })
    }
    ngOnInit(): void {

    }
    zatvori() {
    this.prikaz = !this.prikaz;
    this.otvori.emit(this.prikaz);
    }
    postaviStil(kontrola:string){
    if (this.userPayment.controls[kontrola].invalid && !this.userPayment.controls[kontrola].untouched) {
      return {
        'background-color': 'red',
        'color': 'white'
      }
    } else {
      return {}
    }
    }
    cekiranoPamcenje():boolean{
      let checkbox = document.getElementById('exampleCheck1') as HTMLInputElement;
      if(checkbox.checked){
        return true;
      }
      return false;
    }

  placanje() {
    const validnaForma = this.userPayment.valid;
    if(validnaForma){
      if(this.cekiranoPamcenje()){
        // TODO :: pozvati endpoint za spremanje kartice u bazu (sifrirano)
        alert('Placanje sa spremanjem')
      }else{
        // TODO :: sadrzaj korpe spremiti u tabelu za transakcije
        alert('Placanje')
      }

    }
  }
  restrikcijaUnosaSlova(event:Event){
      const unos = event.target as HTMLInputElement;
      unos.value = unos.value.replace(/[^0-9]/g, '')
  }
  restrikcijaUnosaBrojeva(event:Event){
    const unos = event.target as HTMLInputElement;
    unos.value = unos.value.replace(/[^a-zA-Z ]/g, '')
  }

}
