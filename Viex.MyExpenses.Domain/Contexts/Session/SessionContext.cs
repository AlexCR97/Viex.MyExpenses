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

        public string Email => GetClaimValue(CustomClaims.Email);

        public long UserId
        {
            get {
                var claim = GetClaimValue(CustomClaims.UserId);
                return long.Parse(claim);
            }
        }

        private string GetClaimValue(string claimType)
        {
            var claim = _httpContextAccessor.HttpContext.User.FindFirst(claimType);

            if (claim == null)
                throw new ClaimNotFoundException(claimType);

            return claim.Value;
        }
    }

    public class ClaimNotFoundException : DomainException
    {
        public ClaimNotFoundException(string claimType)
            : base($"Could not find claim of type \"{claimType}\"")
        {
            StatusCode = System.Net.HttpStatusCode.NotFound;
        }
    }
}
