import { CategoryDescriptor } from "@/models/CategoryDescriptor";

export class DescriptorsApi {
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
