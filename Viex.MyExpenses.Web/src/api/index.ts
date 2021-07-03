import { DescriptorsApi } from "./DescriptorsApi";
import { TransactionEntriesApi } from "./TransactionEntriesApi";

export default {
    descriptors: new DescriptorsApi(),
    transactions: new TransactionEntriesApi(),
}
