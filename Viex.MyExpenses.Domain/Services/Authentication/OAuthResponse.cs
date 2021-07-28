using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Domain.Services.Authentication
{
    public class OAuthResponse
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public int Expires_in { get; set; }
        public string Refresh_token { get; set; }
    }
}
