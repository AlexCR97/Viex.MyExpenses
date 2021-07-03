import { TransactionEntry } from "@/models/TransactionEntry";
import { TransactionType } from "@/models/TransactionType";
import arrays from "@/utils/arrays";
import timers from "@/utils/timers";

export class TransactionEntriesApi {
    
    async create(transaction: TransactionEntry) {
        safeParseForPost(transaction)
        await timers.wait(2000)
        return transaction
    }
    
    async search(options?: any): Promise<TransactionEntry[]> {
        return arrays.fromRange(1, 10).map(index => ({
            amount: index * 10,
            categoryId: index,
            category: index % 2 != 0
                ? {
                    categoryDescriptorId: index,
                    description: `Category ${index}`,
                }
                : undefined,
            date: new Date(),
            description: `Transaction Entry #${index}`,
            transactionEntryId: index,
            type: index % 2 == 0
                ? TransactionType.expense
                : TransactionType.income,
        }))
    }
}

function safeParseForPost(transaction: TransactionEntry) {
    transaction.amount = Number(transaction.amount)
    transaction.categoryId = Number(transaction.categoryId)
}
