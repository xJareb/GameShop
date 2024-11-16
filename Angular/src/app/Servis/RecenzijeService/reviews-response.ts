export interface ReviewResponse {
  reviews: Review[]
}

export interface Review {
  userID: number
  content: string
  grade: number
  photoBytes: string
  username: string
  gameID: number
  game: string
}
