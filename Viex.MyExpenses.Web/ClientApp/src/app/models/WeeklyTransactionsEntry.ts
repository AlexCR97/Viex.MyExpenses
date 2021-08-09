import { TransactionEntry } from "./TransactionEntry"

export class WeeklyTransactionsEntry {
    date: Date
    totalExpenses: number
    totalIncome: number
    transactions: TransactionEntry[]
}
