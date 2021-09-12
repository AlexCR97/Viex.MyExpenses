using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Services.Authentication;

namespace Viex.MyExpenses.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _service;

        public AuthController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost("authenticate"), AllowAnonymous]
        public async Task<OAuthResponse> Authenticate([FromBody] SignInModel model) =>
            await _service.SignIn(model);

        [HttpPost("impersonate"), AllowAnonymous]
        public async Task<OAuthResponse> Authenticate([FromBody] ImpersonateRequest request) =>
            await _service.Impersonate(request.UserId);
    }

    public class ImpersonateRequest
    {
        public long UserId { get; set; }
    }
}
