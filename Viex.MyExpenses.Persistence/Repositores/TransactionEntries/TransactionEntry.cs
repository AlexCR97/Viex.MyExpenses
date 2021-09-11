using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Persistence.Repositores.TransactionEntries
{
    public class TransactionEntry : BaseEntity
    {
        public long TransactionEntryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public long? CategoryId { get; set; }
        public CategoryDescriptor Category { get; set; }

        public long TypeId { get; set; }
        public TransactionTypeDescriptor Type { get; set; }

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
