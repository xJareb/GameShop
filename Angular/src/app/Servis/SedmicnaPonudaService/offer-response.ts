export interface OfferResponse {
  games: Game[]
}

export interface Game {
  id: number
  name: string
  photo: string
  percentageDiscount: number
  actionPrice: number
}
