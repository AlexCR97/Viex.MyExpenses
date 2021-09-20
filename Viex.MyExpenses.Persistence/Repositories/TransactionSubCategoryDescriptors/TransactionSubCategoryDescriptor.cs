using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Persistence.Repositories.TransactionCategoryDescriptors;

namespace Viex.MyExpenses.Persistence.Repositories.TransactionSubCategoryDescriptors
{
    public class TransactionSubCategoryDescriptor : BaseDescriptorEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TransactionSubCategoryDescriptorId { get; set; }

        public int TransactionCategoryDescriptorId { get; set; }
        public TransactionCategoryDescriptor TransactionCategoryDescriptor { get; set; }
    }
}
