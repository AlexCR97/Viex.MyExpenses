using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Persistence.Entities;
using Viex.MyExpenses.Persistence.Repositores;

namespace Viex.MyExpenses.Domain.Services.Users
{
    public interface IUserService
    {
        Task<UserModel> Authenticate(AuthenticateModel model);
        Task<UserModel> SignUp(SignUpModel model);
    }

    public class UserService : IUserService
    {
        private readonly IUsersRepository _users;

        public UserService(IUsersRepository users)
        {
            _users = users;
        }

        public async Task<UserModel> Authenticate(AuthenticateModel model)
        {
            var foundUser = await _users.GetFirst(user => user.Email == model.Email);

            if (foundUser == null)
                throw new UserNotFoundException();

            var passwordMatch = foundUser.Password == model.Password;

            if (!passwordMatch)
                throw new AuthenticationFailedException();

            return foundUser.AsModel();
        }

        public async Task<UserModel> SignUp(SignUpModel model)
        {
            ThrowIfEmailTaken(model.Email);

            var createdUserId = await _users.Create(new User
            {
                DateCreated = DateTime.Now,
                Email = model.Email,
                Password = model.Password,
            });

            var createdUser = await _users.GetById(createdUserId);

            return createdUser.AsModel();
        }

        private async void ThrowIfEmailTaken(string email)
        {
            var user = await _users.GetFirst(x => x.Email.Trim().ToLower() == email.Trim().ToLower());

            if (user != null)
                throw new EmailTakenException();
        }
    }
}
