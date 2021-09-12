using Viex.MyExpenses.Persistence.Repositores.CategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositores.TransactionTypeDescriptors;
using Viex.MyExpenses.Persistence.Repositores.Users;

namespace Viex.MyExpenses.Persistence.Repositores.TransactionEntries
{
    public class TransactionEntry : BaseEntity
    {
        public long TransactionEntryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public long? CategoryDescriptorId { get; set; }
        public CategoryDescriptor CategoryDescriptor { get; set; }

        public long TransactionTypeDescriptorId { get; set; }
        public TransactionTypeDescriptor TransactionTypeDescriptor { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
