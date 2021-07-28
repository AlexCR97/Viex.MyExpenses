import { TransactionEntry } from "@/models/TransactionEntry";
import { WeeklyTransactions } from "@/models/WeeklyTransactions";
import { WeeklyTransactionsSearchParams } from "@/models/WeeklyTransactionsSearchParams";
import { isNull } from "@/utils/validators";
import axios from "axios";
import { BaseApi } from "./BaseApi";

export class TransactionEntriesApi extends BaseApi {
    
    endpoint: string = 'transactionEntries';
    
    async create(transaction: TransactionEntry) {
        safeParseForPost(transaction)
        const response = await axios.post<TransactionEntry>(this.uri, transaction)
        return response.data
    }

    async delete(id: number) {
        const uri = `${this.uri}/${id}`
        await axios.delete(uri)
    }

    async deleteAll() {
        const uri = `${this.uri}/all`
        await axios.delete(uri)
    }

    async getWeeklyTransactions(searchParams?: WeeklyTransactionsSearchParams) {
        if (isNull(searchParams))
            searchParams = new WeeklyTransactionsSearchParams()
        
        const uri = `${this.uri}/weekly`
        const response = await axios.post<WeeklyTransactions>(uri, searchParams)
        return response.data
    }

    async importFromCsv(csvFileBase64: string) {
        const uri = `${this.uri}/import`
        await axios.post(uri, { csvFileBase64 })
    }
    
    async search(options?: any): Promise<TransactionEntry[]> {
        const response = await axios.get(this.uri)
        return response.data
    }
}

function safeParseForPost(transaction: TransactionEntry) {
    transaction.amount = Number(transaction.amount)
    transaction.categoryId = Number(transaction.categoryId)
    transaction.typeId = Number(transaction.typeId)
    transaction.userId = Number(transaction.userId)
}
