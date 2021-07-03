<template>
    <v-layout justify-center align-center>
        <v-btn icon>
            <v-icon>mdi-chevron-left</v-icon>
        </v-btn>
        <div class="mx-3">
            <h4 class="font-weight-regular text-center ma-0">
                From <b>{{dateFrom}}</b> to <b>{{dateTo}}</b>
            </h4>
            <p class="text-center ma-0">{{week}}</p>
        </div>
        <v-btn icon>
            <v-icon>mdi-chevron-right</v-icon>
        </v-btn>
    </v-layout>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import dates from '@/utils/dates'

@Component
export default class MyComponent extends Vue {
    
    from = new Date()
    to = new Date()

    get dateFrom() {
        return dates.short(this.from)
    }

    get dateTo() {
        return dates.short(this.to)
    }

    get week() {
        return 'Week 29'
    }
}
</script>
