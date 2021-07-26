import { CategoryDescriptor } from "@/models/CategoryDescriptor";
import { TransactionTypeDescriptor } from "@/models/TransactionTypeDescriptor";
import axios from "axios";
import { BaseApi } from "./BaseApi";

export class DescriptorsApi extends BaseApi {

    endpoint: string = 'descriptors'

    async createCategories(categories: string[]) {
        const uri = `${this.uri}/categories`
        await axios.post(uri, categories)
    }

    async createTransactionTypes(types: string[]) {
        const uri = `${this.uri}/transactionTypes`
        await axios.post(uri, types)
    }

    async getExpenseCategories() {
        const uri = `${this.uri}/categories`
        const response = await axios.get<CategoryDescriptor[]>(uri)
        return response.data
    }

    async getIncomeCategories() {
        const uri = `${this.uri}/categories`
        const response = await axios.get<CategoryDescriptor[]>(uri)
        return response.data
    }

    async getTransactionTypes() {
        const uri = `${this.uri}/transactionTypes`
        const response = await axios.get<TransactionTypeDescriptor[]>(uri)
        return response.data
    }
}
