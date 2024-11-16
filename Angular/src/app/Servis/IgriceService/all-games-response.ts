export interface AllGamesResponse {
  game: Game[]
}

export interface Game {
  id: number
  name: string
  genre: string
  releaseDate: string
  photo: string
  publisher: string
  description: string
  price: number
  percentageDiscount: number
  actionPrice: number
}
