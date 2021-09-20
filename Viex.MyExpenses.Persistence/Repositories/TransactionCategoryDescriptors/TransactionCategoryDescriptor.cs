using System.ComponentModel.DataAnnotations.Schema;
using Viex.MyExpenses.Persistence.Repositories.TransactionTypeDescriptors;

namespace Viex.MyExpenses.Persistence.Repositories.TransactionCategoryDescriptors
{
    public class TransactionCategoryDescriptor : BaseDescriptorEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionCategoryDescriptorId { get; set; }

        public int TransactionTypeDescriptorId { get; set; }
        public TransactionTypeDescriptor TransactionTypeDescriptor { get; set; }
    }
}
