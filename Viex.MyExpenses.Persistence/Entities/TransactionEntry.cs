using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Persistence.Entities
{
    public class TransactionEntry : BaseEntity
    {
        public long TransactionEntryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public long CategoryId { get; set; }
        public CategoryDescriptor Category { get; set; }

        public long TypeId { get; set; }
        public TransactionTypeDescriptor Type { get; set; }
    }
}
