using Microsoft.AspNetCore.Authorization;
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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _users;

        public UsersController(IUserService users)
        {
            _users = users;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IList<UserModel>> GetAll() =>
            await _users.GetAll();

        [HttpPost("signUp"), AllowAnonymous]
        public async Task<UserModel> SignUp([FromBody] SignUpModel model) =>
            await _users.SignUp(model);
    }
}
