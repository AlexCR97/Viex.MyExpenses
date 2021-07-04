using System;
using System.Collections.Generic;
using System.Text;
using Viex.MyExpenses.Persistence.Entities;

namespace Viex.MyExpenses.Domain.Models
{
    public class TransactionTypeDescriptorModel : DescriptorModel
    {
        public long TransactionTypeDescriptorId { get; set; }
    }

    public static class TransactionTypeDescriptorMapper
    {
        public static TransactionTypeDescriptor AsEntity(this TransactionTypeDescriptorModel s)
        {
            return new TransactionTypeDescriptor
            {
                DateCreated = s.DateCreated,
                DateUpdated = s.DateUpdated,
                Description = s.Description,
                TransactionTypeDescriptorId = s.TransactionTypeDescriptorId,
            };
        }

        public static TransactionTypeDescriptorModel AsModel(this TransactionTypeDescriptor s)
        {
            return new TransactionTypeDescriptorModel
            {
                DateCreated = s.DateCreated,
                DateUpdated = s.DateUpdated,
                Description = s.Description,
                TransactionTypeDescriptorId = s.TransactionTypeDescriptorId,
            };
        }
    }
}
