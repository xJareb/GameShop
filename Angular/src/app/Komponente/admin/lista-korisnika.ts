export interface ListaKorisnika {
  korisnici: Korisnici[]
}

export interface Korisnici {
  id: number
  ime: string
  prezime: string
  korisnickoIme: string
  email: string
}
