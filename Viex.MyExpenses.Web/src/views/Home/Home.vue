<template>
    <v-container>

        <PageHeader :actions="actions" class="mb-6"/>

        <DatePager/>
        
        <template v-for="(item, index) of weeklyTransactions">
            <div :key="index" class="mb-4">
                <h2>{{item.day}}</h2>
                <TransactionList :transactions="item.transactions"/>
            </div>
        </template>

    </v-container>
</template>

<script lang="ts">
import { PageHeaderAction } from '@/components/PageHeader'
import { WeeklyTransactions } from '@/models/WeeklyTransactions'
import { Component, Vue } from 'vue-property-decorator'
import api from '@/api'

@Component
export default class HomeComponent extends Vue {
    
    actions: PageHeaderAction[] = [
        {
            icon: 'mdi-plus',
            label: 'New Transaction',
            type: 'text',
            variant: 'primary',
            action: () => {
                console.log("click!")
                this.$router.push('/transaction')
            },
        },
    ]

    weeklyTransactions: WeeklyTransactions[] = []
    loadingTransaction = true

    constructor() {
        super()
        this.init()
    }

    private async init() {
        this.loadingTransaction = true
        
        this.weeklyTransactions = [
            {
                dateFrom: new Date(),
                dateTo: new Date(),
                day: 'Today',
                transactions: await api.transactions.search(),
            },
            {
                dateFrom: new Date(),
                dateTo: new Date(),
                day: 'Yesterday',
                transactions: await api.transactions.search(),
            },
            {
                dateFrom: new Date(),
                dateTo: new Date(),
                day: 'Monday',
                transactions: await api.transactions.search(),
            },
            {
                dateFrom: new Date(),
                dateTo: new Date(),
                day: 'Sunday',
                transactions: await api.transactions.search(),
            },
        ]

        this.loadingTransaction = false
    }
}
</script>

