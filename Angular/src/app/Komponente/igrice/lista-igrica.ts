export interface ListaIgrica {
  igrice: Igrice[]
}

export interface Igrice {
  id: number
  naziv: string
  zanr: string
  datumIzlaska: string
  slika: string
  izdavac: string
  opis: string
  cijena: number
  akcijskaCijena: number
}
