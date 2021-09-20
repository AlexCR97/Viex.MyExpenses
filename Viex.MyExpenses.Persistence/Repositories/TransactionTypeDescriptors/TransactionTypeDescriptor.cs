using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Viex.MyExpenses.Persistence.Repositories.TransactionTypeDescriptors
{
    public class TransactionTypeDescriptor : BaseDescriptorEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionTypeDescriptorId { get; set; }
    }
}
