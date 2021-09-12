using System;
using System.Collections.Generic;
using System.Text;
using Viex.MyExpenses.Persistence.Repositores.Users;

namespace Viex.MyExpenses.Domain.Services.Users
{
    public class UserNotFoundException : DomainException
    {
        public UserNotFoundException()
        {
            StatusCode = System.Net.HttpStatusCode.NotFound;
        }
        
        public UserNotFoundException(long userId)
            : base($"Could not find {nameof(User)} with {nameof(User.UserId)} {userId}")
        {
            StatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}
