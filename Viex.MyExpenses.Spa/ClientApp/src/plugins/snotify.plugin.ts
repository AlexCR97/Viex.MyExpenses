import snotify, { SnotifyPosition } from 'vue-snotify'
import vue from 'vue'
import 'vue-snotify/styles/material.css'

vue.use(snotify, {
    toast: {
        position: SnotifyPosition.rightTop,
    },
})
