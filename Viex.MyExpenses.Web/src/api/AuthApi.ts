import { User } from "@/models/User"
import storage from "@/storage"
import timers from "@/utils/timers"
import axios from "axios"
import { BaseApi } from "./BaseApi"

export class AuthApi extends BaseApi {
    
    endpoint: string = 'auth'

    async authenticate(credentials: { email: string, password: string }) {
        const uri = `${this.uri}/authenticate`
        const response = await axios.post<User>(uri, credentials)
        const user = response.data
        saveUserSession(user)
        return user
    }

    async signOut() {
        await timers.wait(1500)
        storage.removeUser()
    }
}

function saveUserSession(user: User) {
    storage.setUser(user)
}
