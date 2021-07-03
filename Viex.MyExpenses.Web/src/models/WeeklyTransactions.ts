import { TransactionEntry } from "./TransactionEntry";

export interface WeeklyTransactions {
    dateFrom: Date
    dateTo: Date
    day: string
    transactions: TransactionEntry[]
}
