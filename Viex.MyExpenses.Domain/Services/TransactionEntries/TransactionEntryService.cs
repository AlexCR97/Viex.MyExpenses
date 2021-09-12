using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Viex.MyExpenses.Core.Extensions;
using Viex.MyExpenses.Domain.Contexts.Session;
using Viex.MyExpenses.Domain.Mappers;
using Viex.MyExpenses.Domain.Providers.Csv;
using Viex.MyExpenses.Persistence.Repositores.TransactionEntries;
using Viex.MyExpenses.Persistence.Repositores.TransactionTypeDescriptors;

namespace Viex.MyExpenses.Domain.Services.TransactionEntries
{
    public interface ITransactionEntryService
    {
        Task<TransactionEntryModel> Create(TransactionEntryModel transaction);
        Task Delete(long id);
        Task DeleteAll();
        Task<IList<TransactionEntryModel>> GetAll();
        Task<WeeklyTransactions> GetWeeklyTransactions(WeeklyTransactionsSearchParams searchParams);
        Task ImportFromCsv(string csvFileBase64);
        Task<IList<TransactionEntryModel>> Search(TransactionEntrySearchParams searchParams);
    }

    public class TransactionEntryService : ITransactionEntryService
    {
        private readonly ICsvProvider _csv;
        private readonly IModelMapper _modelMapper;
        private readonly ISessionContext _sessionContext;
        private readonly ITransactionEntriesRepository _transactionEntriesRepository;
        private readonly ITransactionTypeDescriptorsRepository _transactionTypeDescriptorsRepository;

        public async Task<TransactionEntryModel> Create(TransactionEntryModel transaction)
        {
            var transactionEntity = await _modelMapper.AsEntity(transaction);
            transactionEntity.DateCreated = DateTime.Now;
            transactionEntity.User = null;
            transactionEntity.UserId = _sessionContext.UserId;
            
            var transactionId = await _transactionEntriesRepository.Create(transactionEntity);
            var createdTransactionEntity = await _transactionEntriesRepository.GetById(transactionId);

            return await _modelMapper.AsModel(createdTransactionEntity);
        }

        public async Task Delete(long id) =>
            await _transactionEntriesRepository.Delete(id);

        public async Task DeleteAll() =>
            await _transactionEntriesRepository.DeleteAll();

        public async Task<IList<TransactionEntryModel>> GetAll()
        {
            var transactions = await _transactionEntriesRepository.GetWhere(user => user.UserId == _sessionContext.UserId);

            return transactions
                .OrderByDescending(x => x.DateCreated)
                .SelectAsync(x => _modelMapper.AsModel(x))
                .AwaitList();
        }

        public async Task<WeeklyTransactions> GetWeeklyTransactions(WeeklyTransactionsSearchParams searchParams)
        {
            if (searchParams == null)
                searchParams = new WeeklyTransactionsSearchParams();

            var firstOfWeek = searchParams.StartDate.HasValue
                ? searchParams.StartDate.Value.Date
                : DateTime.Today.AddDays((int) CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek - (int) DateTime.Today.DayOfWeek).Date;

            var datesOfWeek = Enumerable
                .Range(0, 8)
                .Select(i => firstOfWeek.AddDays(i).Date)
                .ToList();

            var sunday = await Search(new TransactionEntrySearchParams { FromDate = datesOfWeek[0], ToDate = datesOfWeek[1] });
            var monday = await Search(new TransactionEntrySearchParams { FromDate = datesOfWeek[1], ToDate = datesOfWeek[2] });
            var tuesday = await Search(new TransactionEntrySearchParams { FromDate = datesOfWeek[2], ToDate = datesOfWeek[3] });
            var wednesday = await Search(new TransactionEntrySearchParams { FromDate = datesOfWeek[3], ToDate = datesOfWeek[4] });
            var thursday = await Search(new TransactionEntrySearchParams { FromDate = datesOfWeek[4], ToDate = datesOfWeek[5] });
            var friday = await Search(new TransactionEntrySearchParams { FromDate = datesOfWeek[5], ToDate = datesOfWeek[6] });
            var saturday = await Search(new TransactionEntrySearchParams { FromDate = datesOfWeek[6], ToDate = datesOfWeek[7] });

            return new WeeklyTransactions
            {
                Sunday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[0],
                    TotalExpenses = sunday.Where(x => x.TransactionTypeDescriptor == "Expense").Sum(x => x.Amount),
                    TotalIncome = sunday.Where(x => x.TransactionTypeDescriptor == "Income").Sum(x => x.Amount),
                    Transactions = sunday,
                },
                Monday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[1],
                    TotalExpenses = monday.Where(x => x.TransactionTypeDescriptor == "Expense").Sum(x => x.Amount),
                    TotalIncome = monday.Where(x => x.TransactionTypeDescriptor == "Income").Sum(x => x.Amount),
                    Transactions = monday,
                },
                Tuesday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[2],
                    TotalExpenses = tuesday.Where(x => x.TransactionTypeDescriptor == "Expense").Sum(x => x.Amount),
                    TotalIncome = tuesday.Where(x => x.TransactionTypeDescriptor == "Income").Sum(x => x.Amount),
                    Transactions = tuesday,
                },
                Wednesday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[3],
                    TotalExpenses = wednesday.Where(x => x.TransactionTypeDescriptor == "Expense").Sum(x => x.Amount),
                    TotalIncome = wednesday.Where(x => x.TransactionTypeDescriptor == "Income").Sum(x => x.Amount),
                    Transactions = wednesday,
                },
                Thursday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[4],
                    TotalExpenses = thursday.Where(x => x.TransactionTypeDescriptor == "Expense").Sum(x => x.Amount),
                    TotalIncome = thursday.Where(x => x.TransactionTypeDescriptor == "Income").Sum(x => x.Amount),
                    Transactions = thursday,
                },
                Friday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[5],
                    TotalExpenses = friday.Where(x => x.TransactionTypeDescriptor == "Expense").Sum(x => x.Amount),
                    TotalIncome = friday.Where(x => x.TransactionTypeDescriptor == "Income").Sum(x => x.Amount),
                    Transactions = friday,
                },
                Saturday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[6],
                    TotalExpenses = saturday.Where(x => x.TransactionTypeDescriptor == "Expense").Sum(x => x.Amount),
                    TotalIncome = saturday.Where(x => x.TransactionTypeDescriptor == "Income").Sum(x => x.Amount),
                    Transactions = saturday,
                },
            };
        }

        public async Task ImportFromCsv(string csvFileBase64)
        {
            var records = await _csv.Parse<TransactionCsvRow>(csvFileBase64);

            foreach (var row in records)
            {
                var type = await _transactionTypeDescriptorsRepository.GetFirst(x => x.Description == row.Type);

                await _transactionEntriesRepository.Create(new TransactionEntry
                {
                    Amount = decimal.Parse(row.Amount, NumberStyles.Currency),
                    DateCreated = DateTime.Parse(row.Date),
                    Description = row.Description,
                    TransactionTypeDescriptorId = type.TransactionTypeDescriptorId,
                    UserId = _sessionContext.UserId,
                });
            }
        }

        public async Task<IList<TransactionEntryModel>> Search(TransactionEntrySearchParams searchParams)
        {
            var transactions = await _transactionEntriesRepository.Search(searchParams.AsQueryParams());

            return transactions
                .OrderByDescending(x => x.DateCreated)
                .SelectAsync(x => _modelMapper.AsModel(x))
                .AwaitList();
        }
    }
}
