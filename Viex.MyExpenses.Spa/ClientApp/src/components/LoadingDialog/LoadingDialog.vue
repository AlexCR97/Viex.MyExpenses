<template>
    <v-dialog v-model="opened" max-width="400" persistent>
        <v-card>
            <v-card-title v-if="options.title">{{options.title}}</v-card-title>
            <v-divider v-if="options.title"/>

            <v-card-text class="py-6">
                <p class="text-center ma-0 mb-6">{{options.message}}</p>
                <v-layout justify-center>
                    <v-progress-circular indeterminate size="48"/>
                </v-layout>
            </v-card-text>
        </v-card>
    </v-dialog>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import { LoadingDialogOptions } from './LoadingDialogOptions'

@Component
export default class LoadingDialogComponent extends Vue {
    
    opened = false

    options: LoadingDialogOptions = {
        title: 'Loading',
        message: 'Please wait..',
    };

    open(options?: LoadingDialogOptions) {
        if (options) {
            if (options.title)
                this.options.title = options.title

            if (options.message)
                this.options.message = options.message
        }

        this.opened = true
    }

    close() {
        this.opened = false
    }
}
</script>
