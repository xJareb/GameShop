export interface CartListResponse {
  cart: Cart[]
}

export interface Cart {
  id: number
  name: string
  photo: string
  price: number
  actionPrice: number
  quantity: number
}
