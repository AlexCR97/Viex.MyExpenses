using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Domain.Services.TransactionEntries;
using Viex.MyExpenses.Domain.Services.TransactionTypeDescriptors;
using Viex.MyExpenses.Persistence.Repositores.CategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositores.TransactionEntries;
using Viex.MyExpenses.Persistence.Repositores.TransactionTypeDescriptors;
using Viex.MyExpenses.Persistence.Repositores.Users;

namespace Viex.MyExpenses.Domain.Mappers
{
    public interface IModelMapper
    {
        Task<TransactionEntry> AsEntity(TransactionEntryModel model);
        Task<TransactionEntryModel> AsModel(TransactionEntry entity);

        Task<User> AsEntity(UserModel model);
        Task<UserModel> AsModel(User entity);
    }

    public class ModelMapper : IModelMapper
    {
        private readonly ICategoryDescriptorsRepository _categoryDescriptorsRepository;
        private readonly ITransactionEntriesRepository _transactionEntriesRepository;
        private readonly ITransactionTypeDescriptorsRepository _transactionTypeDescriptorsRepository;
        private readonly IUsersRepository _usersRepository;

        #region TransactionEntry

        public async Task<TransactionEntry> AsEntity(TransactionEntryModel model)
        {
            var entity = new TransactionEntry
            {
                Amount = model.Amount,
                DateCreated = model.DateCreated,
                DateUpdated = model.DateUpdated,
                Description = model.Description,
                TransactionEntryId = model.TransactionEntryId,
                User = await AsEntity(model.User),
                UserId = model.UserId,
            };

            entity.CategoryDescriptorId = await GetCategoryDescriptorId(model.CategoryDescriptor);
            entity.TransactionTypeDescriptorId = await GetTransactionTypeDescriptorId(model.TransactionTypeDescriptor);

            return entity;
        }

        public async Task<TransactionEntryModel> AsModel(TransactionEntry entity) => new()
        {
            Amount = entity.Amount,
            CategoryDescriptor = entity.CategoryDescriptor?.Description,
            DateCreated = entity.DateCreated,
            DateUpdated = entity.DateUpdated,
            Description = entity.Description,
            TransactionEntryId = entity.TransactionEntryId,
            TransactionTypeDescriptor = entity.TransactionTypeDescriptor?.Description,
            User = await AsModel(entity.User),
            UserId = entity.UserId,
        };

        #endregion

        #region User

        public async Task<User> AsEntity(UserModel model)
        {
            var entity = new User
            {
                DateCreated = model.DateCreated,
                DateUpdated = model.DateUpdated,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserId = model.UserId,
                UserName = model.UserName,
            };

            var userSnapshot = await _usersRepository.GetById(model.UserId);

            if (userSnapshot != null)
                entity.Password = userSnapshot.Password;

            return entity;
        }

        public async Task<UserModel> AsModel(User entity) => new()
        {
            DateCreated = entity.DateCreated,
            DateUpdated = entity.DateUpdated,
            Email = entity.Email,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            UserId = entity.UserId,
            UserName = entity.UserName,
        };

        #endregion

        #region Helpers

        private async Task<long?> GetCategoryDescriptorId(string description)
        {
            var descriptor = await _categoryDescriptorsRepository.GetByDescription(description);
            return descriptor?.CategoryDescriptorId;
        }

        private async Task<long> GetTransactionTypeDescriptorId(string description)
        {
            var descriptor = await _transactionTypeDescriptorsRepository.GetByDescription(description);

            if (descriptor == null)
                throw new TransactionTypeDescriptorNotFoundException();

            return descriptor.TransactionTypeDescriptorId;
        }

        #endregion
    }
}
