import { User } from "@/models/User";
import jwtDecode from "jwt-decode";

const keys = {
    accessToken: 'viex.expenses.accessToken',
    user: 'viex.expenses.user',
}

export default {
    getAccessToken() {
        return localStorage.getItem(keys.accessToken)
    },

    getUserId() {
        const accessToken = this.getAccessToken();
        const claims = jwtDecode<any>(accessToken);
        return Number(claims.userId)
    },

    removeAccessToken() {
        localStorage.removeItem(keys.accessToken)
    },

    setAccessToken(accessToken: string) {
        localStorage.setItem(keys.accessToken, accessToken)
    }
}

function getObject<T>(key: string) {
    const json = localStorage.getItem(key)
    return JSON.parse(json) as T
}

function setObject(key: string, object: any) {
    const json = JSON.stringify(object)
    localStorage.setItem(keys.user, json)
}
