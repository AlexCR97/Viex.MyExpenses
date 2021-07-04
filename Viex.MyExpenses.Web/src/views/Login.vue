<template>
<!--eslint-disable-->
    <div>
        <v-layout justify-center align-center style="height: 100vh">
            <v-card width="500">
                <v-card-text>
                    <h1 class="text-center mb-10">My Expenses</h1>
                    <v-layout justify-center>
                        <v-btn class="text-capitalize px-12" color="primary" @click="onLoginClicked">Login</v-btn>
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
import { Component, Vue } from "vue-property-decorator";

@Component
export default class LoginComponent extends Vue {

    onLoginClicked() {
        this.$router.push('/home');
    }

    onLoginAsDeveloperClicked() {
        this.$router.push('/developer');
    }
    
}
</script>
