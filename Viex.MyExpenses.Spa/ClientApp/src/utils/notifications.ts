import vue from 'vue'

export default {
    success(message: string) {
        vue.prototype.$snotify.success(message, {
            timeout: 2000,
            showProgressBar: true,
            //closeOnClick: false,
            //pauseOnHover: true,
        })
    },
}
