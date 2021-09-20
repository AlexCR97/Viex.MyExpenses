using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viex.MyExpenses.Core.Extensions;
using Viex.MyExpenses.Domain.Mappers;
using Viex.MyExpenses.Domain.Services.TransactionSubCategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositories;
using Viex.MyExpenses.Persistence.Repositories.RoleDescriptors;
using Viex.MyExpenses.Persistence.Repositories.TransactionCategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositories.TransactionSubCategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositories.TransactionTypeDescriptors;

namespace Viex.MyExpenses.Domain.Services
{
    public interface IDescriptorService
    {
        Task CreateRoles(IEnumerable<string> roles);
        Task CreateTransactionCategories(IEnumerable<string> categories);
        Task CreateTransactionSubCategories(IEnumerable<TransactionSubCategoryDescriptorModel> descriptions);
        Task CreateTransactionTypes(IEnumerable<string> transactionTypes);
        void DropCategories();
        void DropRoles();
        void DropTransactionTypes();
        Task<IList<string>> GetCategories();
        Task<IList<string>> GetRoles();
        Task<IList<string>> GetTransactionTypes();
    }

    public class DescriptorService : IDescriptorService
    {
        private readonly IModelMapper _modelMapper;
        private readonly IRoleDescriptorsRepository _roleDescriptorsRepository;
        private readonly ITransactionCategoryDescriptorRepository _transactionCategoryDescriptorsRepository;
        private readonly ITransactionSubCategoryDescriptorRepository _transactionSubCategoryDescriptorRepository;
        private readonly ITransactionTypeDescriptorsRepository _transactionTypeDescriptorsRepository;

        public async Task CreateRoles(IEnumerable<string> roles)
        {
            var descriptors = roles.Select(description => new RoleDescriptor
            {
                DateCreated = DateTime.Now,
                Description = description,
            });

            await _roleDescriptorsRepository.CreateAll(descriptors);
        }

        public async Task CreateTransactionCategories(IEnumerable<string> categories)
        {
            var categoryDescriptors = categories.Select(category => new TransactionCategoryDescriptor
            {
                DateCreated = DateTime.Now,
                Description = category,
            });

            await _transactionCategoryDescriptorsRepository.CreateAll(categoryDescriptors);
        }

        public async Task CreateTransactionSubCategories(IEnumerable<TransactionSubCategoryDescriptorModel> descriptors)
        {
            var entities = descriptors.SelectAsync(async descriptor =>
            {
                var entity = await _modelMapper.AsEntity(descriptor);
                entity.DateCreated = DateTime.Now;
                return entity;
            }).AwaitList();

            await _transactionSubCategoryDescriptorRepository.CreateAll(entities);
        }

        public async Task CreateTransactionTypes(IEnumerable<string> transactionTypes)
        {
            var transactionTypeDescriptors = transactionTypes.Select(type => new TransactionTypeDescriptor
            {
                Description = type,
            });

            await _transactionTypeDescriptorsRepository.CreateAll(transactionTypeDescriptors);
        }

        public void DropCategories() =>
            _transactionCategoryDescriptorsRepository.DropAll();

        public void DropRoles() =>
            _roleDescriptorsRepository.DropAll();

        public void DropTransactionTypes() =>
            _transactionTypeDescriptorsRepository.DropAll();

        public async Task<IList<string>> GetCategories() =>
            await GetDescriptors(_transactionCategoryDescriptorsRepository);

        public async Task<IList<string>> GetRoles() =>
            await GetDescriptors(_roleDescriptorsRepository);

        public async Task<IList<string>> GetTransactionTypes() =>
            await GetDescriptors(_transactionTypeDescriptorsRepository);

        private async Task<IList<string>> GetDescriptors<T>(IDescriptorRepository<T> repository) where T : BaseDescriptorEntity
        {
            var descriptors = await repository.Get();
            return descriptors.Select(x => x.Description).ToList();
        }
    }
}
