export interface LoggedUser {
  user: User[]
}

export interface User {
  id: number
  name: string
  surname: string
  username: string
  email: string
  dateBirth: string
  photoBytes: any
  numberOfPurchase: number
  googlePhoto: string
}
