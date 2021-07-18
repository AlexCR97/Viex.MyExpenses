import { User } from "@/models/User";

const keys = {
    user: 'viex.expenses.user',
}

export default {
    getUserId() {
        const user = getObject<User>(keys.user)
        return user.userId
    },

    removeUser() {
        localStorage.removeItem(keys.user)
    },

    setUser(user: User) {
        setObject(keys.user, user)
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
