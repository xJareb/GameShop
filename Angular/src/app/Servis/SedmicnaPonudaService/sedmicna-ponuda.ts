export interface SedmicnaPonuda {
  igrice: Igrice[]
}

export interface Igrice {
  id: number
  naziv: string
  slika: string
  postotakAkcije: number
  akcijskaCijena: number
}
