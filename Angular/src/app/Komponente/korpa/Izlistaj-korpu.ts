export interface IzlistajKorpu {
  korpa: Korpa[]
}

export interface Korpa {
  id: number
  naziv: string
  slika: string
  pravaCijena: number
  akcijskaCijena: number
  kolicina: number
}
