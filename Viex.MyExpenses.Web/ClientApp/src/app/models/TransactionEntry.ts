import { BaseModel } from "./BaseModel"
import { CategoryDescriptor } from "./CategoryDescriptor"
import { TransactionTypeDescriptor } from "./TransactionTypeDescriptor"
import { User } from "./User"

export class TransactionEntry extends BaseModel {
    
    transactionEntryId: number
    amount: number
    description: string

    categoryId: number
    category: CategoryDescriptor

    typeId: number
    type: TransactionTypeDescriptor

    userId: number
    user: User

    constructor() {
        super()
        this.amount = 0
        this.dateCreated = new Date()
    }
}
