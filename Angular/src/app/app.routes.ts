import { Routes } from '@angular/router';
import {RegistracijaComponent} from "./Komponente/registracija/registracija.component";
import {IgriceComponent} from "./Komponente/igrice/igrice.component";
import {DetaljiIgriceComponent} from "./Komponente/detalji-igrice/detalji-igrice.component";
import {PocetnaComponent} from "./Komponente/pocetna/pocetna.component";
import {PrijavaComponent} from "./Komponente/prijava/prijava.component";
import {KorisnikComponent} from "./Komponente/korisnik/korisnik.component";
import {AdminComponent} from "./Komponente/admin/admin.component";
import {KontaktComponent} from "./Komponente/kontakt/kontakt.component";
import {KorpaComponent} from "./Komponente/korpa/korpa.component";
import {KupnjeComponent} from "./Komponente/kupnje/kupnje.component";

export const routes: Routes = [
  {path: '',redirectTo:'/pocetna',pathMatch:'full'},
  {path: 'registracija', component:RegistracijaComponent},
  {path: 'igrice', component:IgriceComponent},
  {path:'detalji-igrice/:id',component:DetaljiIgriceComponent},
  {path:'pocetna',component: PocetnaComponent},
  {path:'prijava', component:PrijavaComponent},
  {path:'korisnik', component:KorisnikComponent},
  {path: 'admin',component:AdminComponent},
  {path:'kontakt', component:KontaktComponent},
  {path:'korpa', component:KorpaComponent},
  {path:'kupnje', component:KupnjeComponent},
];
