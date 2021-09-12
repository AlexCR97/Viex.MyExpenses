import { TransactionEntry } from "./TransactionEntry.model"

export class WeeklyTransactionsEntry {
    date: Date
    totalExpenses: number
    totalIncome: number
    transactions: TransactionEntry[]
}
