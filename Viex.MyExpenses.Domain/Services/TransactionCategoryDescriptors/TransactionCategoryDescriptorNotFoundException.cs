using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Persistence.Repositories.TransactionCategoryDescriptors;

namespace Viex.MyExpenses.Domain.Services.TransactionCategoryDescriptors
{
    public class TransactionCategoryDescriptorNotFoundException : DomainException
    {
        public TransactionCategoryDescriptorNotFoundException(string description)
            : base($"Could not find {nameof(TransactionCategoryDescriptor)} with {nameof(TransactionCategoryDescriptor.Description)} {description}")
        {
            StatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}
