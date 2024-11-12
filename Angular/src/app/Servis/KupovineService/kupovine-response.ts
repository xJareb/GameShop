export interface KupovineResponse {
  kupovine: Kupovine[]
}

export interface Kupovine {
  id: number
  datumKupovine: string
  korisnikID: number
  korisnik: string
  igrice: Igrice[]
}

export interface Igrice {
  id: number
  naziv: string
  slika: string
  akcijskaCijena: number
}
