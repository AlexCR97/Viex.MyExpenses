import { CategoryDescriptor } from "@/models/CategoryDescriptor";
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
        const categories: string[] = [
            "Food",
            "Fitness",
            "Home",
            "Health",
            "Hobbies",
            "Technology",
            "Others",
        ];

        return new Promise<CategoryDescriptor[]>(resolve => resolve(categories.map((category, index) => ({
            categoryDescriptorId: index + 1,
            description: category,
        }))));
    }

    async getIncomeCategories() {
        const categories: string[] = [
            "Salary",
            "Bonus",
            "Gifts",
            "Prizes",
            "Others",
        ];

        return new Promise<CategoryDescriptor[]>(resolve => resolve(categories.map((category, index) => ({
            categoryDescriptorId: index + 1,
            description: category,
        }))));
    }
}
