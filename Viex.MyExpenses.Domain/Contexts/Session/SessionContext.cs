using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Domain.Contexts.Session
{
    public interface ISessionContext
    {
        string Email { get; }
        long UserId { get; }
    }

    public class SessionContext : ISessionContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string Email
        {
            get {
                try
                {
                    var claim = _httpContextAccessor.HttpContext.User.FindFirst(claim => claim.Type.Contains("email"));
                    return claim.Value;
                }
                catch
                {
                    throw new EmailClaimException();
                }
            }
        }

        public long UserId
        {
            get {
                try
                {
                    var claim = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaims.UserId);
                    return long.Parse(claim.Value);
                }
                catch
                {
                    throw new UserIdClaimException();
                }
            }
        }
    }
}
