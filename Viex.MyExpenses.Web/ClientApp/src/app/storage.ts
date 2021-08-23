import jwtDecode from "jwt-decode"

const StorageItems = {
    local: {
        accessToken: 'viex.myexpenses.accessToken',
    },
}

export default {
    local: {
        getAccessToken() {
            return getAccessToken()
        },

        getUserId() {
            const accessToken = getAccessToken()
            const claims = jwtDecode<any>(accessToken)
            return Number(claims.userId)
        },

        removeAccessToken() {
            localStorage.removeItem(StorageItems.local.accessToken)
        },

        setAccessToken(accessToken: string) {
            localStorage.setItem(StorageItems.local.accessToken, accessToken)
        },
    },
}

function getAccessToken() {
    return localStorage.getItem(StorageItems.local.accessToken)
}