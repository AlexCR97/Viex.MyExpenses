using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Domain.Services.Users
{
    public class UserNotFoundException : DomainException
    {
        public UserNotFoundException()
        {
            StatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}
