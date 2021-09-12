using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Services.Descriptors;

namespace Viex.MyExpenses.Domain.Services.TransactionTypeDescriptors
{
    public class TransactionTypeDescriptorNotFoundException : DomainException
    {
        public TransactionTypeDescriptorNotFoundException()
            : base($"Could not find descriptor of type \"{DescriptorTypes.TransactionTypes}\"")
        {
            StatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}
