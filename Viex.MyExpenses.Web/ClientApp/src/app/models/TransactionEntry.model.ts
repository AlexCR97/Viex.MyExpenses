import { Validator } from "fluentvalidation-ts"
import { ValidationErrors } from "fluentvalidation-ts/dist/ValidationErrors"
import { isNull } from "../utils/validators"
import { BaseModel } from "./BaseModel.model"
import { User } from "./User.model"

export class TransactionEntry extends BaseModel {
    
    transactionEntryId: number
    amount: number
    description: string
    
    categoryDescriptor: string
    transactionTypeDescriptor: string
    
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

        this.ruleFor('categoryDescriptor')
            .notNull()
            .notEmpty()
            .withMessage('You must select a category')
            
        this.ruleFor('transactionTypeDescriptor')
            .notNull()
            .notEmpty()
            .withMessage('You must select a type')
    }

    isValid(validations: ValidationErrors<TransactionEntry>) {
        return (true
            && isNull(validations.amount)
            && isNull(validations.description)
            && isNull(validations.categoryDescriptor)
            && isNull(validations.transactionTypeDescriptor)
        )
    }
}
