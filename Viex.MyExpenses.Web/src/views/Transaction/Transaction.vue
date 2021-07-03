<template>
    <v-container>
        
        <PageHeader :color="titleColor" :icon="titleIcon" :title="title" :actions="actions" class="mb-8"/>

        <v-select filled label="Transaction Type *" :disabled="isSavingTransaction()" :items="transactionTypes" v-model="transaction.type"/>

        <v-layout>
            <v-text-field filled label="Amount *" :disabled="isSavingTransaction()" prepend-inner-icon="mdi-currency-usd" type="number" style="max-width: 200px" class="mr-4" v-model="transaction.amount"/>
            <v-text-field filled label="Description *" :disabled="isSavingTransaction()" v-model="transaction.description"/>
        </v-layout>

        <v-select filled label="Category" :disabled="isSavingTransaction()" :items="categoryItems" item-text="description" item-value="categoryDescriptorId" v-model="transaction.categoryId"/>

    </v-container>
</template>

<script lang="ts">
import api from '@/api';
import { PageHeaderAction } from '@/components/PageHeader'
import { CategoryDescriptor } from '@/models/CategoryDescriptor';
import { TransactionEntry } from '@/models/TransactionEntry'
import { TransactionType, TransactionTypesArray } from '@/models/TransactionType'
import { isNullOrZero, notNullNorWhitespace, notNullNorZero } from '@/utils/validators'
import { Component, Vue } from 'vue-property-decorator'

@Component
export default class TransactionComponent extends Vue {
    
    transaction = new TransactionEntry()
    loadingTransaction = false
    savingTransaction = false

    transactionTypes = TransactionTypesArray
    expenseCategories: CategoryDescriptor[] = []
    incomeCategories: CategoryDescriptor[] = []

    actions: PageHeaderAction[] = [
        {
            icon: 'mdi-content-save',
            label: 'Save',
            variant: 'primary',
            action: () => this.onSaveClicked(),
            disabled: () => !this.isTransactionValid() || this.isSavingTransaction(),
            loading: () => this.isSavingTransaction(),
        }
    ]

    constructor() {
        super()
        this.init()
    }

    get categoryItems() {
        if (this.transaction.type == TransactionType.expense)
            return this.expenseCategories

        if (this.transaction.type == TransactionType.income)
            return this.incomeCategories

        return []
    }

    get isTransactionTypeExpense() {
        return this.transaction.type == TransactionType.expense
    }

    get isTransactionTypeIncome() {
        return this.transaction.type == TransactionType.income
    }

    isTransactionValid() {
        return (true
            && notNullNorWhitespace(this.transaction.type)
            && this.transaction.amount > 0
            && notNullNorWhitespace(this.transaction.description)
            && notNullNorZero(this.transaction.categoryId)
        );
    }

    isSavingTransaction() {
        return this.savingTransaction
    }

    get title() {
        const operation = this.isNewTransaction
            ? 'New'
            : 'Updating'

        return `${operation} ${this.transaction.type}`
    }

    get titleColor() {
        if (this.transaction.type == TransactionType.expense)
            return 'red'
        
        if (this.transaction.type == TransactionType.income)
            return 'green'

        return 'gray'
    }

    get titleIcon() {
        if (this.transaction.type == TransactionType.expense)
            return 'mdi-arrow-down'
        
        if (this.transaction.type == TransactionType.income)
            return 'mdi-arrow-up'

        return 'gray'
    }

    private get isNewTransaction() {
        return isNullOrZero(this.transaction.transactionEntryId)
    }

    private async init() {
        api.descriptors.getExpenseCategories().then(descriptors => this.expenseCategories = descriptors)
        api.descriptors.getIncomeCategories().then(descriptors => this.incomeCategories = descriptors)
    }

    private async onSaveClicked() {
        console.log("this.transaction:", this.transaction);
        this.savingTransaction = true
        await api.transactions.create(this.transaction)
        this.savingTransaction = false
        // TODO Show toast
        this.$router.replace('/home')
    }
}
</script>
