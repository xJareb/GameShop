export interface PurchasesResponse {
  purchases: Purchase[]
}

export interface Purchase {
  id: number
  purchaseDate: string
  userID: number
  user: string
  games: Game[]
}

export interface Game {
  id: number
  name: string
  photo: string
  actionPrice: number
}
