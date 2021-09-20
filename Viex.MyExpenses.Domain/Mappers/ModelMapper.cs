using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Domain.Services.Descriptors;
using Viex.MyExpenses.Domain.Services.TransactionCategoryDescriptors;
using Viex.MyExpenses.Domain.Services.TransactionEntries;
using Viex.MyExpenses.Domain.Services.TransactionSubCategoryDescriptors;
using Viex.MyExpenses.Domain.Services.TransactionTypeDescriptors;
using Viex.MyExpenses.Persistence.Repositories.TransactionCategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositories.TransactionEntries;
using Viex.MyExpenses.Persistence.Repositories.TransactionSubCategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositories.TransactionTypeDescriptors;
using Viex.MyExpenses.Persistence.Repositories.Users;

namespace Viex.MyExpenses.Domain.Mappers
{
    public interface IModelMapper
    {
        Task<TransactionEntry> AsEntity(TransactionEntryModel model);
        Task<TransactionEntryModel> AsModel(TransactionEntry entity);

        Task<TransactionSubCategoryDescriptor> AsEntity(TransactionSubCategoryDescriptorModel model);
        Task<TransactionSubCategoryDescriptorModel> AsModel(TransactionSubCategoryDescriptor entity);

        Task<User> AsEntity(UserModel model);
        Task<UserModel> AsModel(User entity);
    }

    public class ModelMapper : IModelMapper
    {
        private readonly ITransactionCategoryDescriptorRepository _transactionCategoryDescriptorsRepository;
        private readonly ITransactionSubCategoryDescriptorRepository _transactionSubCategoryDescriptorRepository;
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
                TransactionCategoryDescriptorOther = model.TransactionCategoryDescriptorOther,
                TransactionEntryId = model.TransactionEntryId,
                TransactionSubCategoryDescriptorOther = model.TransactionSubCategoryDescriptorOther,
                User = await AsEntity(model.User),
                UserId = model.UserId,
            };

            entity.TransactionCategoryDescriptorId = await GetTransactionCategoryDescriptorId(model.TransactionCategoryDescriptor);
            entity.TransactionSubCategoryDescriptorId = await GetTransactionSubCategoryDescriptorId(model.TransactionSubCategoryDescriptor);

            return entity;
        }

        public async Task<TransactionEntryModel> AsModel(TransactionEntry entity) => new()
        {
            Amount = entity.Amount,
            DateCreated = entity.DateCreated,
            DateUpdated = entity.DateUpdated,
            Description = entity.Description,
            TransactionCategoryDescriptor = entity.TransactionCategoryDescriptor.Description,
            TransactionCategoryDescriptorOther = entity.TransactionCategoryDescriptorOther,
            TransactionSubCategoryDescriptorOther = entity.TransactionSubCategoryDescriptorOther,
            TransactionEntryId = entity.TransactionEntryId,
            TransactionSubCategoryDescriptor = entity.TransactionSubCategoryDescriptor?.Description,
            User = await AsModel(entity.User),
            UserId = entity.UserId,
        };

        #endregion

        #region TransactionSubCategoryDescriptor

        public async Task<TransactionSubCategoryDescriptor> AsEntity(TransactionSubCategoryDescriptorModel model)
        {
            var categorySnapshot = await _transactionCategoryDescriptorsRepository.GetByDescription(model.TransactionCategoryDescriptor);

            if (categorySnapshot == null)
                throw new TransactionCategoryDescriptorNotFoundException(model.TransactionCategoryDescriptor);

            var subCategoryDescriptorEntity = new TransactionSubCategoryDescriptor
            {
                Description = model.Description,
                TransactionCategoryDescriptor = categorySnapshot,
                TransactionCategoryDescriptorId = categorySnapshot.TransactionCategoryDescriptorId,
            };

            var subCategorySnapshot = await _transactionSubCategoryDescriptorRepository.GetByDescription(model.Description);

            if (subCategorySnapshot != null)
            {
                subCategoryDescriptorEntity.DateCreated = subCategorySnapshot.DateCreated;
                subCategoryDescriptorEntity.DateUpdated = subCategorySnapshot.DateUpdated;
                subCategoryDescriptorEntity.TransactionSubCategoryDescriptorId = subCategorySnapshot.TransactionSubCategoryDescriptorId;
            }

            return subCategoryDescriptorEntity;
        }

        public async Task<TransactionSubCategoryDescriptorModel> AsModel(TransactionSubCategoryDescriptor entity) => new()
        {
            Description = entity.Description,
            TransactionCategoryDescriptor = entity.TransactionCategoryDescriptor.Description,
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

        private async Task<int> GetTransactionCategoryDescriptorId(string description)
        {
            var descriptor = await _transactionCategoryDescriptorsRepository.GetByDescription(description);

            if (descriptor == null)
                throw new TransactionTypeDescriptorNotFoundException(); // TODO Throw proper exception

            return descriptor.TransactionCategoryDescriptorId;
        }

        private async Task<int?> GetTransactionSubCategoryDescriptorId(string description)
        {
            var descriptor = await _transactionSubCategoryDescriptorRepository.GetByDescription(description);
            return descriptor?.TransactionSubCategoryDescriptorId;
        }

        #endregion
    }
}
