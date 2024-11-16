export interface GameUpdateRequest {
  gameID: number
  name: string
  genreID: number
  releaseDate: Date
  photo: string
  publisher: string
  description: string
  price: number
  percentageDiscount: number
}
