export interface GameCategoriesResponse {
  games: Game[]
}

export interface Game {
  id: number
  name: string
  genreID: number
  genre: string
  releaseDate: string
  photo: string
  publisher: string
  description: string
  price: number
  percentageDiscount: number
  actionPrice: number
}
