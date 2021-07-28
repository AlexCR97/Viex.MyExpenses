<template>
    <v-list>
        <TransactionItem v-for="(item, index) of transactions"
            class="mb-4"
            :key="index"
            :transaction="item"
            @transactionDeleted="onTransactionDeleted(item)"/>
    </v-list>
</template>

<script lang="ts">
import { TransactionEntry } from '@/models/TransactionEntry'
import { Component, Prop, Vue } from 'vue-property-decorator'

@Component
export default class TransactionListComponent extends Vue {
    
    @Prop({ default: () => [] }) transactions: TransactionEntry[]

    onTransactionDeleted(transaction: TransactionEntry) {
        const index = this.transactions.indexOf(transaction)
        this.transactions.splice(index, 1)
    }
}
</script>
