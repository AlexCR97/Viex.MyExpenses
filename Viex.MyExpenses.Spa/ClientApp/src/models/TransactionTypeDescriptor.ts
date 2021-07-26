import { BaseDescriptor } from "./BaseDescriptor"

export class TransactionTypeDescriptor extends BaseDescriptor {
    transactionTypeDescriptorId: number
}

export enum TransactionType {
    income = 'Income',
    expense = 'Expense',
}
