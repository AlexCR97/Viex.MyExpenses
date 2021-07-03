<template>
    <v-card class="px-8 py-6 elevation-4">
        <v-layout align-center>
            <v-icon class="mr-4" size="40">{{icon}}</v-icon>
            <h1 class="ma-0 mr-auto" :style="{ color: color }">{{title}}</h1>
            <v-btn v-for="(item, index) of actions" :key="index"
                :text="item.type == 'text'"
                :color="item.variant"
                class="text-capitalize"
                :disabled="item.disabled != undefined && item.disabled != null ? item.disabled() : false"
                :loading="item.loading != undefined && item.loading != null ? item.loading() : false"
                @click="item.action()">
                <v-icon left>{{item.icon}}</v-icon>
                {{item.label}}
            </v-btn>
        </v-layout>
    </v-card>
</template>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator';
import { PageHeaderAction } from './PageHeaderAction'

@Component
export default class PageHeaderComponent extends Vue {
    @Prop() color: string
    @Prop({ default: 'mdi-home' }) icon: string
    @Prop({ default: 'Home' }) title: string
    @Prop({ default: () => [] }) actions: PageHeaderAction[]
}
</script>
