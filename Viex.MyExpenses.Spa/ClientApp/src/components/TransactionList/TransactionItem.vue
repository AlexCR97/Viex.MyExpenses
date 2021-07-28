<template>
    <v-list-item class="elevation-1">
        <span :class="clazz" style="min-width: 70px; text-align: right;">
            {{amount}}
        </span>
        <v-list-item-content>
            <v-list-item-title>{{transaction.description}}</v-list-item-title>
            <v-list-item-subtitle v-if="hasCategory">{{transaction.category.description}}</v-list-item-subtitle>
        </v-list-item-content>
        <v-list-item-action>
            <v-menu>
                <template v-slot:activator="{ on, attrs }">
                    <v-btn icon v-bind="attrs" v-on="on">
                        <v-icon>mdi-dots-vertical</v-icon>
                    </v-btn>
                </template>
                <v-list>
                    <v-list-item v-list-item v-for="(item, index) in actions" :key="index" @click="item.action()">
                        <v-list-item-title :class="item.labelClass">{{item.label}}</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>
        </v-list-item-action>

        <ConfirmDialog ref="confirmDialog"/>
        <LoadingDialog ref="loadingDialog"/>

    </v-list-item>
</template>

<style scoped>
.expense {
    color: red;
}

.income {
    color: green;
}
</style>

<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import { TransactionEntry } from '@/models/TransactionEntry'
import { notNull } from '@/utils/validators'
import { TransactionItemAction } from './TransactionItemAction'
import { TransactionType } from '@/models/TransactionTypeDescriptor'
import ConfirmDialogComponent from '../ConfirmDialog/ConfirmDialog.vue'
import api from '@/api'
import timers from '@/utils/timers'
import LoadingDialogComponent from '../LoadingDialog/LoadingDialog.vue'
import notifications from '@/utils/notifications'

@Component
export default class TransactionItemComponent extends Vue {

    @Prop() transaction: TransactionEntry

    actions: TransactionItemAction[] = [
        {
            label: 'Edit',
            action: () => {}
        },
        {
            label: 'Delete',
            labelClass: 'error--text',
            action: () => this.onDeleteClicked(),
        },
    ]

    get amount(): string {
        const operator = this.isExpense
            ? '-'
            : '+'
        
        return `${operator} $${this.transaction.amount}`
    }

    get clazz() {
        return {
            'expense': this.isExpense,
            'income': this.isIncome,
            'mr-4': true,
        }
    }

    get confirmDialog() {
        return this.$refs.confirmDialog as ConfirmDialogComponent
    }

    get hasCategory() {
        return notNull(this.transaction.category)
    }

    get isExpense() {
        return this.transaction.type.description == TransactionType.expense
    }

    get isIncome() {
        return this.transaction.type.description == TransactionType.income
    }

    get loadingDialog() {
        return this.$refs.loadingDialog as LoadingDialogComponent;
    }

    private async onDeleteClicked() {
        try {
            await this.confirmDialog.open({
                title: 'Delete Transaction?',
                message: 'Do you really want to delete this transaction?',
                confirmText: 'Delete Transaction',
            })
            
            this.loadingDialog.open({
                title: 'Deleting Transaction',
            })
            
            await timers.wait(1500)
            notifications.success('Transaction deleted')
            await api.transactions.delete(this.transaction.transactionEntryId)
            this.$emit('transactionDeleted')
        }
        finally {
            // TODO Handle error
            this.loadingDialog.close()
        }
    }
}
</script>
