import { AuthApi } from "./AuthApi";
import { DescriptorsApi } from "./DescriptorsApi";
import { TransactionEntriesApi } from "./TransactionEntriesApi";
import { UsersApi } from "./UsersApi";

export default {
    auth: new AuthApi(),
    descriptors: new DescriptorsApi(),
    transactions: new TransactionEntriesApi(),
    users: new UsersApi(),
}
