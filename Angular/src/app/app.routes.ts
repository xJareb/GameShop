import { Routes } from '@angular/router';
import {RegistracijaComponent} from "./Komponente/registration/registracija.component";
import {IgriceComponent} from "./Komponente/igrice/igrice.component";
import {DetaljiIgriceComponent} from "./Komponente/game-details/detalji-igrice.component";
import {PocetnaComponent} from "./Komponente/home-page/pocetna.component";
import {PrijavaComponent} from "./Komponente/login/prijava.component";
import {KorisnikComponent} from "./Komponente/korisnik/korisnik.component";
import {AdminComponent} from "./Komponente/admin/admin.component";
import {KontaktComponent} from "./Komponente/contact/kontakt.component";
import {KorpaComponent} from "./Komponente/korpa/korpa.component";
import {KupnjeComponent} from "./Komponente/kupnje/kupnje.component";
import {PlacanjeComponent} from "./Komponente/korpa/placanje/placanje.component";
import {AktivacijaComponent} from "./Komponente/korpa/aktivacija/aktivacija.component";

export const routes: Routes = [
  {path: '',redirectTo:'/home-page',pathMatch:'full'},
  {path: 'registration', component:RegistracijaComponent},
  {path: 'igrice', component:IgriceComponent},
  {path:'game-details/:id',component:DetaljiIgriceComponent},
  {path:'home-page',component: PocetnaComponent},
  {path:'login', component:PrijavaComponent},
  {path:'korisnik', component:KorisnikComponent},
  {path: 'admin',component:AdminComponent},
  {path:'contact', component:KontaktComponent},
  {path:'korpa', component:KorpaComponent},
  {path:'kupnje', component:KupnjeComponent},
  {path:'placanje', component:PlacanjeComponent},
  {path:'aktivacija', component: AktivacijaComponent}
];
