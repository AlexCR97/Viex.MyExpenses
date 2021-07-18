<template>
    <v-container>

        <PageHeader :actions="actions" class="mb-6"/>

        <DatePager @startDateChanged="onStartDateChanged" class="mb-6"/>
        
        <h2 class="mb-2">This week's summary</h2>
        
        <div class="ml-8">
            <h3>Total Expenses</h3>
            <p class="red--text">{{money(totalWeekExpenses)}}</p>

            <h3>Total Income</h3>
            <p class="green--text">{{money(totalWeekIncome)}}</p>
        </div>

        <v-expansion-panels multiple v-model="expansionPanelModel">
            <v-expansion-panel v-for="(item, index) of weeklyTransactions" :key="index" class="mb-4">
                
                <v-expansion-panel-header>
                    <h3 class="font-weight-regular">
                        <b class="mr-2">{{getDayName(item)}}</b>
                        {{getDateShort(item)}}
                    </h3>
                </v-expansion-panel-header>
                
                <v-expansion-panel-content>
                    <p v-if="!hasTransactions(item)" style="color: gray">No transactions</p>

                    <div v-if="hasTransactions(item)">
                        <v-row>
                            <v-col>
                                <h4 class="text-center">Total Expenses</h4>
                                <p class="text-center red--text">{{money(item.totalExpenses)}}</p>
                            </v-col>
                            <v-col>
                                <h4 class="text-center">Total Income</h4>
                                <p class="text-center green--text">{{money(item.totalIncome)}}</p>
                            </v-col>
                        </v-row>
                        <TransactionList :transactions="item.transactions"/>
                    </div>
                </v-expansion-panel-content>
            </v-expansion-panel>
        </v-expansion-panels>

    </v-container>
</template>

<script lang="ts">
import { PageHeaderAction } from '@/components/PageHeader'
import { WeeklyTransactionsSearchParams } from '@/models/WeeklyTransactionsSearchParams'
import { Component, Vue } from 'vue-property-decorator'
import api from '@/api'
import { notNullNorEmpty } from '@/utils/validators'
import { WeeklyTransactionsEntry } from '@/models/WeeklyTransactionsEntry'
import dates from '@/utils/dates'
import arrays from '@/utils/arrays'
import strings from '@/utils/strings'

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

    weeklyTransactions: WeeklyTransactionsEntry[] = []
    loadingTransaction = true
    searchParams = new WeeklyTransactionsSearchParams();

    expansionPanelModel = arrays.fromRange(0, 7);

    constructor() {
        super()
        this.getWeeklyTransactions()
    }

    get totalWeekExpenses() {
        return this.weeklyTransactions
            .map(x => x.totalExpenses)
            .reduce((prev, current) => prev + current, 0);
    }

    get totalWeekIncome() {
        return this.weeklyTransactions
            .map(x => x.totalIncome)
            .reduce((prev, current) => prev + current, 0);
    }

    getDayName(item: WeeklyTransactionsEntry) {
        return dates.dayName(item.date)
    }

    getDateShort(item: WeeklyTransactionsEntry) {
        return dates.short(item.date);
    }

    hasTransactions(item: WeeklyTransactionsEntry) {
        return notNullNorEmpty(item.transactions);
    }

    money(num: number) {
        return strings.currency(num);
    }

    onStartDateChanged(date: Date) {
        this.searchParams.startDate = date;
        this.getWeeklyTransactions()
    }

    private async getWeeklyTransactions() {
        this.loadingTransaction = true
        
        const weeklyTransactions = await api.transactions.getWeeklyTransactions(this.searchParams)
        
        this.weeklyTransactions = [
            weeklyTransactions.saturday,
            weeklyTransactions.friday,
            weeklyTransactions.thursday,
            weeklyTransactions.wednesday,
            weeklyTransactions.tuesday,
            weeklyTransactions.monday,
            weeklyTransactions.sunday,
        ]
        
        this.loadingTransaction = false
    }
}
</script>
