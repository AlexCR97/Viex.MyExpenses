import { Validator } from "fluentvalidation-ts"
import { ValidationErrors } from "fluentvalidation-ts/dist/ValidationErrors"
import { isNull } from "../utils/validators"
import { BaseModel } from "./BaseModel"
import { CategoryDescriptor } from "./CategoryDescriptor"
import { TransactionTypeDescriptor } from "./TransactionTypeDescriptor"
import { User } from "./User"

export class TransactionEntry extends BaseModel {
    
    transactionEntryId: number
    amount: number
    description: string
    category: string
    type: string
    
    userId: number
    user: User

    constructor() {
        super()
        this.amount = 0
        this.dateCreated = new Date()
    }
}

export class TransactionEntryValidator extends Validator<TransactionEntry> {
    constructor() {
        super()

        this.ruleFor('amount')
            .notNull()
            .must(amount => Number(amount) > 0)
            .withMessage('Amount must be greater than 0')

        this.ruleFor('description')
            .notNull()
            .notEmpty()
            .withMessage('Description cannot be empty')

        this.ruleFor('category')
            .notNull()
            .notEmpty()
            .withMessage('You must select a category')
            
        this.ruleFor('type')
            .notNull()
            .notEmpty()
            .withMessage('You must select a type')
    }

    isValid(validations: ValidationErrors<TransactionEntry>) {
        return (true
            && isNull(validations.amount)
            && isNull(validations.description)
            && isNull(validations.category)
            && isNull(validations.type)
        )
    }
}
