import { OAuthResponse } from "@/models/OAuthResponse"
import { User } from "@/models/User"
import storage from "@/storage"
import timers from "@/utils/timers"
import axios from "axios"
import jwtDecode from "jwt-decode"
import { BaseApi } from "./BaseApi"

export class AuthApi extends BaseApi {
    
    endpoint: string = 'auth'

    async authenticate(credentials: { email: string, password: string }) {
        const uri = `${this.uri}/authenticate`

        const body = {
            email: credentials.email,
            password: credentials.password,
            grantType: "client_credentials",
        }

        const response = await axios.post<OAuthResponse>(uri, body)
        const oAuthResponse = response.data
        storage.setAccessToken(oAuthResponse.access_token)
    }

    async signOut() {
        await timers.wait(1000)
        storage.removeAccessToken()
    }
}
