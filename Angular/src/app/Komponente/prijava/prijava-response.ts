export interface PrijavaResponse {
  autentifikacijaToken: AutentifikacijaToken
  isLogiran: boolean
}

export interface AutentifikacijaToken {
  id: number
  vrijednost: string
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
}
