using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viex.MyExpenses.Core.Extensions;
using Viex.MyExpenses.Domain.Mappers;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Persistence.Repositories.Users;

namespace Viex.MyExpenses.Domain.Services.Users
{
    public interface IUserService
    {
        Task<UserModel> Authenticate(AuthenticateModel model);
        Task<IList<UserModel>> GetAll();
        Task<UserModel> SignUp(SignUpModel model);
    }

    public class UserService : IUserService
    {
        private readonly IModelMapper _modelMapper;
        private readonly IUsersRepository _usersRepository;

        public UserService(IModelMapper modelMapper, IUsersRepository usersRepository)
        {
            _modelMapper = modelMapper;
            _usersRepository = usersRepository;
        }

        public async Task<UserModel> Authenticate(AuthenticateModel model)
        {
            var foundUser = await _usersRepository.GetFirst(user => user.Email == model.Email);

            if (foundUser == null)
                throw new UserNotFoundException();

            var passwordMatch = foundUser.Password == model.Password;

            if (!passwordMatch)
                throw new AuthenticationFailedException();

            return await _modelMapper.AsModel(foundUser);
        }

        public async Task<IList<UserModel>> GetAll()
        {
            var users = await _usersRepository.GetWhere(_ => true);
            return users
                .SelectAsync(user => _modelMapper.AsModel(user))
                .AwaitList();
        }

        public async Task<UserModel> SignUp(SignUpModel model)
        {
            ThrowIfEmailTaken(model.Email);

            var createdUserId = await _usersRepository.Create(new User
            {
                DateCreated = DateTime.Now,
                Email = model.Email,
                Password = model.Password,
            });

            var createdUser = await _usersRepository.GetById(createdUserId);

            return await _modelMapper.AsModel(createdUser);
        }

        private async void ThrowIfEmailTaken(string email)
        {
            var user = await _usersRepository.GetFirst(x => x.Email.Trim().ToLower() == email.Trim().ToLower());

            if (user != null)
                throw new EmailTakenException();
        }
    }
}
