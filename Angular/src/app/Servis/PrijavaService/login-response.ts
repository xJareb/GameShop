export interface LoginResponse {
  authenticationToken: AuthenticationToken
  isLogged: boolean
}

export interface AuthenticationToken {
  id: number
  value: string
  userID: number
  userAccountID: number
  userAccount: UserAccount
  timeOfRecording: string
  ipAdress: string
}

export interface UserAccount {
  id: number
  username: string
  email: string
  password: string
  birthDate: string
  isAdmin: boolean
  isUser: boolean
  isDeleted: boolean
  isBlackList: boolean
  isGoogleProvider: boolean
}
