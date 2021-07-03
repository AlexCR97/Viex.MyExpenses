import { CategoryDescriptor } from "./CategoryDescriptor"
import { TransactionType } from "./TransactionType"

export class TransactionEntry {
    
    transactionEntryId: number
    amount: number
    categoryId: number
    category: CategoryDescriptor
    date: Date
    description: string
    type: TransactionType

    constructor() {
        this.amount = 0
        this.date = new Date()
        this.type = TransactionType.expense
    }
}
