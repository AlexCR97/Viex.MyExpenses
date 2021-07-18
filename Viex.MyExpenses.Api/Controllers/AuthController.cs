using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Domain.Services.Users;

namespace Viex.MyExpenses.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _users;

        public AuthController(IUserService users)
        {
            _users = users;
        }

        [HttpPost("authenticate")]
        public async Task<UserModel> Authenticate([FromBody] AuthenticateModel model)
        {
            return await _users.Authenticate(model);
        }
    }
}
