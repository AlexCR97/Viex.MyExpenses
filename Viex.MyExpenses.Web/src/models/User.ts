import { BaseModel } from "./BaseModel"

export class User extends BaseModel {
    userId: number
    email: string
    password: string
    firstName: string
    lastName: string
    userName: string
}
