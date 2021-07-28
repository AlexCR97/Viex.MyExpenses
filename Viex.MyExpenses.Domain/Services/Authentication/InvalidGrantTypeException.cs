using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Domain.Services.Authentication
{
    public class InvalidGrantTypeException : DomainException
    {
        public InvalidGrantTypeException()
        {
            StatusCode = System.Net.HttpStatusCode.Unauthorized;
        }
    }
}
