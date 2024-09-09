import { Routes } from '@angular/router';
import {RegistracijaComponent} from "./Komponente/registracija/registracija.component";
import {IgriceComponent} from "./Komponente/igrice/igrice.component";
import {DetaljiIgriceComponent} from "./Komponente/detalji-igrice/detalji-igrice.component";

export const routes: Routes = [
  {path: 'registracija', component:RegistracijaComponent},
  {path: 'igrice', component:IgriceComponent},
  {path:'detalji-igrice/:id',component:DetaljiIgriceComponent}
];
