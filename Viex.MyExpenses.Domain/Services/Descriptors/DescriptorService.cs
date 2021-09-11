﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Persistence.Entities;
using Viex.MyExpenses.Persistence.Repositores;

namespace Viex.MyExpenses.Domain.Services
{
    public interface IDescriptorService
    {
        Task CreateCategories(IEnumerable<string> categories);
        Task CreateTransactionTypes(IEnumerable<string> transactionTypes);
        Task DropCategories();
        Task DropTransactionTypes();
        Task<IList<CategoryDescriptorModel>> GetCategories();
        Task<IList<TransactionTypeDescriptorModel>> GetTransactionTypes();
    }

    public class DescriptorService : IDescriptorService
    {
        private readonly ICategoryDescriptorsRepository _categories;
        private readonly ITransactionTypeDescriptorsRepository _transactionTypes;

        public DescriptorService(ICategoryDescriptorsRepository categories, ITransactionTypeDescriptorsRepository transactionTypes)
        {
            _categories = categories;
            _transactionTypes = transactionTypes;
        }

        public async Task CreateCategories(IEnumerable<string> categories)
        {
            var categoryDescriptors = categories.Select(category => new CategoryDescriptor
            {
                Description = category,
            });

            foreach (var descriptor in categoryDescriptors)
                await _categories.Create(descriptor);
        }

        public async Task CreateTransactionTypes(IEnumerable<string> transactionTypes)
        {
            var transactionTypeDescriptors = transactionTypes.Select(type => new TransactionTypeDescriptor
            {
                Description = type,
            });

            foreach (var descriptor in transactionTypeDescriptors)
                await _transactionTypes.Create(descriptor);
        }

        public async Task DropCategories() =>
            await _categories.cle

        public Task DropTransactionTypes()
        {
            throw new NotImplementedException();
        }

        public async Task<IList<CategoryDescriptorModel>> GetCategories()
        {
            var categories = await _categories.GetWhere(_ => true);
            return categories.Select(x => x.AsModel()).ToList();
        }

        public async Task<IList<TransactionTypeDescriptorModel>> GetTransactionTypes()
        {
            var types = await _transactionTypes.GetWhere(_ => true);
            return types.Select(type => type.AsModel()).ToList();
        }
    }
}
