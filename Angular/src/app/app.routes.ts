import { Routes } from '@angular/router';
import {RegistracijaComponent} from "./Komponente/registracija/registracija.component";
import {IgriceComponent} from "./Komponente/igrice/igrice.component";
import {DetaljiIgriceComponent} from "./Komponente/detalji-igrice/detalji-igrice.component";
import {PocetnaComponent} from "./Komponente/pocetna/pocetna.component";

export const routes: Routes = [
  {path: '',redirectTo:'/pocetna',pathMatch:'full'},
  {path: 'registracija', component:RegistracijaComponent},
  {path: 'igrice', component:IgriceComponent},
  {path:'detalji-igrice/:id',component:DetaljiIgriceComponent},
  {path:'pocetna',component: PocetnaComponent}
];
