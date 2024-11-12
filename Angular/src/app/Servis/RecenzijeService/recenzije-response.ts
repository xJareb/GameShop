export interface RecenzijeResponse {
  recenzije: Recenzije[]
}

export interface Recenzije {
  korisnikID: number
  sadrzaj: string
  ocjena: number
  slika: string
  korisnickoIme: string
  igricaID: number
  igrica: string
}

