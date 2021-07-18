using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Domain.Services.Users
{
    public class AuthenticationFailedException : DomainException
    {
        public AuthenticationFailedException()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest;
        }
    }
}
