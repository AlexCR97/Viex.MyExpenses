<template>
    <v-list>
        <TransactionItem v-for="(item, index) of transactions" :transaction="item" :key="index" class="mb-4"/>
    </v-list>
</template>

<script lang="ts">
import { TransactionEntry } from '@/models/TransactionEntry'
import { Component, Prop, Vue } from 'vue-property-decorator'

@Component
export default class TransactionListComponent extends Vue {
    @Prop({ default: () => [] }) transactions: TransactionEntry[]
}
</script>
