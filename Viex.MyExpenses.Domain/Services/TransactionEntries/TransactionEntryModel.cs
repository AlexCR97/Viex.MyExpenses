using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Persistence.Repositores.TransactionEntries;

namespace Viex.MyExpenses.Domain.Services.TransactionEntries
{
    public class TransactionEntryModel : BaseModel
    {
        public long TransactionEntryId { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public string CategoryDescriptor { get; set; }
        public string TransactionTypeDescriptor { get; set; }

        public long UserId { get; set; }
        public UserModel User { get; set; }
    }
}
