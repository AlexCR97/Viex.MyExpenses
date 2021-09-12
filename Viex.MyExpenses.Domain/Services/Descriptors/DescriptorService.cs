using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Viex.MyExpenses.Persistence.Repositores;
using Viex.MyExpenses.Persistence.Repositores.CategoryDescriptors;
using Viex.MyExpenses.Persistence.Repositores.TransactionTypeDescriptors;

namespace Viex.MyExpenses.Domain.Services
{
    public interface IDescriptorService
    {
        Task CreateCategories(IEnumerable<string> categories);
        Task CreateTransactionTypes(IEnumerable<string> transactionTypes);
        void DropCategories();
        void DropTransactionTypes();
        Task<IList<string>> GetCategories();
        Task<IList<string>> GetTransactionTypes();
    }

    public class DescriptorService : IDescriptorService
    {
        private readonly ICategoryDescriptorsRepository _categoryDescriptorsRepository;
        private readonly ITransactionTypeDescriptorsRepository _transactionTypeDescriptorsRepository;

        public DescriptorService(ICategoryDescriptorsRepository categoryDescriptorsRepository, ITransactionTypeDescriptorsRepository transactionTypeDescriptorsRepository)
        {
            _categoryDescriptorsRepository = categoryDescriptorsRepository;
            _transactionTypeDescriptorsRepository = transactionTypeDescriptorsRepository;
        }

        public async Task CreateCategories(IEnumerable<string> categories)
        {
            var categoryDescriptors = categories.Select(category => new CategoryDescriptor
            {
                DateCreated = DateTime.Now,
                Description = category,
            });

            await _categoryDescriptorsRepository.CreateAll(categoryDescriptors);
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
            _categoryDescriptorsRepository.DropAll();

        public void DropTransactionTypes() =>
            _transactionTypeDescriptorsRepository.DropAll();

        public async Task<IList<string>> GetCategories() =>
            await GetDescriptors(_categoryDescriptorsRepository);

        public async Task<IList<string>> GetTransactionTypes() =>
            await GetDescriptors(_transactionTypeDescriptorsRepository);

        private async Task<IList<string>> GetDescriptors<T>(IDescriptorRepository<T> repository) where T : BaseDescriptorEntity
        {
            var descriptors = await repository.Get();
            return descriptors.Select(x => x.Description).ToList();
        }
    }
}
