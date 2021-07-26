<template>
    <div>
        <h2 class="text-center mb-2">{{month}}</h2>

        <v-layout justify-center align-center>
            <v-btn icon @click="onPreviousClicked" :disabled="isPreviousDisabled">
                <v-icon>mdi-chevron-left</v-icon>
            </v-btn>
            <div class="mx-3">
                <h4 class="font-weight-regular text-center ma-0">
                    From <b>{{dateFrom}}</b> to <b>{{dateTo}}</b>
                </h4>
                <p class="text-center ma-0">{{week}}</p>
            </div>
            <v-btn icon @click="onNextClicked" :disabled="isNextDisabled">
                <v-icon>mdi-chevron-right</v-icon>
            </v-btn>
        </v-layout>

        <v-layout justify-center class="mt-2">
            <v-btn color="primary" text @click="onTodayClicked">Today</v-btn>
        </v-layout>
    </div>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import dates from '@/utils/dates'

@Component
export default class MyComponent extends Vue {
    
    startDate = dates.firstOfCurrentWeek()
    endDate = dates.lastOfCurrentWeek()
    currentWeek = dates.currentWeek()

    constructor() {
        super();
    }

    get dateFrom() {
        return dates.short(this.startDate)
    }

    get dateTo() {
        return dates.short(this.endDate)
    }

    get isPreviousDisabled() {
        return this.currentWeek <= 1;
    }

    get isNextDisabled() {
        return this.currentWeek >= 52;
    }

    get month() {
        return dates.monthName(this.startDate)
    }

    get week() {
        return `Week ${this.currentWeek}`
    }

    onPreviousClicked() {
        if (this.currentWeek <= 1)
            return

        this.currentWeek -= 1
        this.moveDates(-7)
    }

    onNextClicked() {
        if (this.currentWeek >= 52)
            return

        this.currentWeek += 1
        this.moveDates(7)
    }

    onTodayClicked() {
        this.setToday();
    }

    private moveDates(days: number) {
        this.startDate = dates.addDays(this.startDate, days)
        this.endDate = dates.addDays(this.endDate, days)
        this.emitDatesChanged()
    }

    private setToday() {
        this.startDate = dates.firstOfCurrentWeek()
        this.endDate = dates.lastOfCurrentWeek()
        this.currentWeek = dates.currentWeek()
        this.emitDatesChanged()
    }

    private emitDatesChanged() {
        this.$emit('startDateChanged', this.startDate)
    }
}
</script>
