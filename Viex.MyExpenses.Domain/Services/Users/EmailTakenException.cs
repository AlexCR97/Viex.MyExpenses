using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Domain.Services.Users
{
    public class EmailTakenException : DomainException
    {
        public EmailTakenException()
        {
            StatusCode = System.Net.HttpStatusCode.BadRequest;
        }
    }
}
