using System.ComponentModel.DataAnnotations.Schema;

namespace Viex.MyExpenses.Persistence.Repositories.TransactionTypeDescriptors
{
    public class TransactionTypeDescriptor : BaseDescriptorEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionTypeDescriptorId { get; set; }
    }
}
