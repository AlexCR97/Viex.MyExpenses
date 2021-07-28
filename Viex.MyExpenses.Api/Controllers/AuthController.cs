using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Domain.Services.Authentication;
using Viex.MyExpenses.Domain.Services.Users;

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
        public async Task<OAuthResponse> Authenticate([FromBody] SignInModel model)
        {
            return await _service.SignIn(model);
        }
    }
}
