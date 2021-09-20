using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Persistence.Repositories.TransactionSubCategoryDescriptors;

namespace Viex.MyExpenses.Domain.Services.TransactionSubCategoryDescriptors
{
    public class TransactionSubCategoryDescriptorNotFoundException : DomainException
    {
        public TransactionSubCategoryDescriptorNotFoundException(string description)
            : base($"Could not find {nameof(TransactionSubCategoryDescriptor)} with {nameof(TransactionSubCategoryDescriptor.Description)} {description}")
        {
            StatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}
