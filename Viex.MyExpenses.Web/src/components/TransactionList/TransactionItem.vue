<template>
    <v-list-item class="app-hover-scale-sm elevation-1">
        <v-list-item-avatar :class="clazz">
            {{amount}}
        </v-list-item-avatar>
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
                        <v-list-item-title>{{item.label}}</v-list-item-title>
                    </v-list-item>
                </v-list>
            </v-menu>
        </v-list-item-action>
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
import { TransactionType } from '@/models/TransactionType'
import { notNull } from '@/utils/validators'
import { TransactionItemAction } from './TransactionItemAction'

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
            action: () => {}
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
        }
    }

    get hasCategory() {
        return notNull(this.transaction.category)
    }

    get isExpense() {
        return this.transaction.type == TransactionType.expense
    }

    get isIncome() {
        return this.transaction.type == TransactionType.income
    }
}
</script>
