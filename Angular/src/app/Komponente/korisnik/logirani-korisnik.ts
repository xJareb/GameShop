export interface LogiraniKorisnik {
  korisnik: Korisnik[]
}

export interface Korisnik {
  id: number
  ime: string
  prezime: string
  korisnickoIme: string
  email: string
  datumRodjenja: string
}
