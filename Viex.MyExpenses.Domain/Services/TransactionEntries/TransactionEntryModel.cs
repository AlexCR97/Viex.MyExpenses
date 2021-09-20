using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Persistence.Repositories.TransactionEntries;

namespace Viex.MyExpenses.Domain.Services.TransactionEntries
{
    public class TransactionEntryModel : BaseModel
    {
        public long TransactionEntryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string TransactionCategoryDescriptorOther { get; set; }
        public string TransactionSubCategoryDescriptorOther { get; set; }

        public string TransactionCategoryDescriptor { get; set; }
        public string TransactionSubCategoryDescriptor { get; set; }

        public int UserId { get; set; }
        public UserModel User { get; set; }
    }
}
