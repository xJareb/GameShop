import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-upozorenje-modal',
  standalone: true,
  imports: [],
  templateUrl: './upozorenje-modal.component.html',
  styleUrl: './upozorenje-modal.component.css'
})
export class UpozorenjeModalComponent {
  @Input() naslov!:string;
  @Input() sadrzaj!:string;
  @Output() otvori = new EventEmitter<boolean>();
  private prikaz: boolean = true;

  zatvori() {
    this.prikaz = !this.prikaz;
    this.otvori.emit(this.prikaz);
  }

}
