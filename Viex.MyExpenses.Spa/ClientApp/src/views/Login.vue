<template>
<!--eslint-disable-->
    <div>
        <v-layout justify-center align-center style="height: 100vh">
            <v-card width="500">
                <v-card-text>

                    <h1 class="text-center mb-10">My Expenses</h1>
                    
                    <v-text-field filled prepend-inner-icon="mdi-mail" label="Email" v-model="credentials.email"/>
                    <v-text-field filled prepend-inner-icon="mdi-lock" label="Password" v-model="credentials.password"/>

                    <v-layout justify-center class="mb-4">
                        <v-btn class="text-capitalize" color="primary" :disabled="authenticating" :loading="authenticating" width="250" @click="onLoginClicked">Login</v-btn>
                    </v-layout>

                    <v-layout justify-center class="mb-4">
                        <v-btn class="text-capitalize" color="error" :disabled="authenticating" :loading="authenticating" text width="250" @click="onSignUpClicked">Sign Up</v-btn>
                    </v-layout>

                </v-card-text>
            </v-card>
        </v-layout>

        <v-footer fixed class="py-4">
            <p class="ma-0">
                <span class="app-hover-pointer" @click="onLoginAsDeveloperClicked">
                    Login as developer
                </span>
            </p>
        </v-footer>
    </div>
</template>

<script lang="ts">
/* eslint-disable */
import api from "@/api";
import { Component, Vue } from "vue-property-decorator";

@Component
export default class LoginComponent extends Vue {

    credentials = {
        email: '',
        password: '',
    }

    authenticating = false

    onLoginClicked() {
        this.authenticate(this.credentials.email, this.credentials.password)
    }

    onLoginAsDeveloperClicked() {
        this.authenticate('developer@viex.com', '123456')
    }

    onSignUpClicked() {
        this.signUp(this.credentials.email, this.credentials.password)
    }
    
    private async authenticate(email: string, password: string) {
        try {
            this.authenticating = true
            await api.auth.authenticate({ email, password })
            this.$router.push('/home')
        } catch {
            // TODO Handle error
        } finally {
            this.authenticating = false
        }
    }

    private async signUp(email: string, password: string) {
        try {
            this.authenticating = true
            await api.users.signUp({ email, password })
        } catch {
            // TODO Handle error
        } finally {
            this.authenticating = false
        }

        this.authenticate(email, password)
    }
}
</script>
