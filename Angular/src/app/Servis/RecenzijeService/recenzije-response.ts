export interface RecenzijeResponse {
  recenzije: Recenzije[]
}

export interface Recenzije {
  sadrzaj: string
  ocjena: number
  slika: any
  korisnickoIme: string
  igrica: string
}
