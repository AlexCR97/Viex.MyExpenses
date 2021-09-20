using System.ComponentModel.DataAnnotations.Schema;
using Viex.MyExpenses.Persistence.Repositories.TransactionCategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositories.TransactionSubCategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositories.TransactionTypeDescriptors;
using Viex.MyExpenses.Persistence.Repositories.Users;

namespace Viex.MyExpenses.Persistence.Repositories.TransactionEntries
{
    public class TransactionEntry : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long TransactionEntryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string TransactionCategoryDescriptorOther { get; set; }
        public string TransactionSubCategoryDescriptorOther { get; set; }

        public int TransactionCategoryDescriptorId { get; set; }
        public TransactionCategoryDescriptor TransactionCategoryDescriptor { get; set; }

        public int? TransactionSubCategoryDescriptorId { get; set; }
        public TransactionSubCategoryDescriptor TransactionSubCategoryDescriptor { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}
