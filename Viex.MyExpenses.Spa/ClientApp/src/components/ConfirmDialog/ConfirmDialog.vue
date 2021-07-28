<template>
    <v-dialog v-model="opened" max-width="400">
        <v-card>
            <v-card-title>{{options.title}}</v-card-title>
            <v-divider/>

            <v-card-text class="py-6">
                <p class="ma-0">{{options.message}}</p>
            </v-card-text>

            <v-divider/>
            <v-card-actions>
                <v-spacer/>
                <v-btn text class="text-capitalize ml-3" color="primary" @click="onConfirmClicked">
                    {{options.confirmText}}
                </v-btn>
                <v-btn text class="text-capitalize ml-3" color="error" @click="onCancelClicked">
                    {{options.cancelText}}
                </v-btn>
            </v-card-actions>
        </v-card>
    </v-dialog>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import { ConfirmDialogOptions } from './ConfirmDialogOptions'

@Component
export default class ConfirmDialogComponent extends Vue {
    
    opened = false

    options: ConfirmDialogOptions = {
        title: 'Are you sure?',
        message: 'Do you really want to perform this action?',
        confirmText: 'Confirm',
        cancelText: 'Cancel',
    };

    private resolve: any = null
    private reject: any = null

    open(options?: ConfirmDialogOptions) {
        if (options) {
            if (options.title)
                this.options.title = options.title

            if (options.message)
                this.options.message = options.message

            if (options.confirmText)
                this.options.confirmText = options.confirmText

            if (options.cancelText)
                this.options.cancelText = options.cancelText
        }


        return new Promise<void>((resolve, reject) => {
            this.resolve = resolve
            this.reject = reject
            this.opened = true
        })
    }

    onConfirmClicked() {
        this.opened = false
        this.resolve()
    }

    onCancelClicked() {
        this.opened = false
        this.reject()
    }
}
</script>
