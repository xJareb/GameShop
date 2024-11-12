export interface PrijavaResponse {
  autentifikacijaToken: AutentifikacijaToken
  isLogiran: boolean
}

export interface AutentifikacijaToken {
  id: number
  vrijednost: string
  KorisnikID:number
  korisnickiNalogID: number
  korisnickiNalog: KorisnickiNalog
  vrijemeEvidentiranja: string
  ipAdresa: string
}

export interface KorisnickiNalog {
  id: number
  korisnickoIme: string
  email: string
  lozinka: string
  datumRodjenja: string
  isAdmin: boolean
  isKorisnik: boolean
  isDeleted:boolean
  isBlackList:boolean
  isGoogleProvider:boolean
}
