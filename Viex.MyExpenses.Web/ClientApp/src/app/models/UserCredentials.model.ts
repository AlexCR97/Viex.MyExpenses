import { Validator } from "fluentvalidation-ts"
import { ValidationErrors } from "fluentvalidation-ts/dist/ValidationErrors";
import { isNull } from "../utils/validators";

export class UserCredentials {
    email: string
    password: string
}

export class UserCredentialsValidator extends Validator<UserCredentials> {
    constructor() {
        super();

        this.ruleFor('email')
            .notNull().withMessage('Enter your email')
            .notEmpty().withMessage('Enter your email')
            .emailAddress().withMessage('Enter a valid email address')
        
        this.ruleFor('password')
            .notNull().withMessage('Enter your password')
            .notEmpty().withMessage('Enter your password')
    }

    isValid(validations: ValidationErrors<UserCredentials>) {
        return (true
            && isNull(validations.email)
            && isNull(validations.password)
        )
    }
}
