import { User } from "@/models/User"
import axios from "axios"
import { BaseApi } from "./BaseApi"

export class UsersApi extends BaseApi {
    
    endpoint: string = 'users'

    async signUp(model: { email: string, password: string }) {
        const uri = `${this.uri}/signUp`
        const response = await axios.post<User>(uri, model)
        return response.data
    }
}
