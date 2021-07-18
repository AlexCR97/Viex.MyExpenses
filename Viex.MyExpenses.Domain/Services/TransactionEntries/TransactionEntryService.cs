using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Viex.MyExpenses.Domain.Models;
using Viex.MyExpenses.Domain.Providers.Csv;
using Viex.MyExpenses.Persistence.Entities;
using Viex.MyExpenses.Persistence.Repositores;
using Viex.MyExpenses.Persistence.Repositores.TransactionEntries;

namespace Viex.MyExpenses.Domain.Services.TransactionEntries
{
    public interface ITransactionEntryService
    {
        Task<TransactionEntryModel> Create(TransactionEntryModel transaction);
        Task DeleteAll();
        Task<IEnumerable<TransactionEntryModel>> GetAll();
        Task<WeeklyTransactions> GetWeeklyTransactions(WeeklyTransactionsSearchParams searchParams);
        Task ImportFromCsv(string csvFileBase64);
        Task<IList<TransactionEntryModel>> Search(TransactionEntrySearchParams searchParams);
    }

    public class TransactionEntryService : ITransactionEntryService
    {
        private readonly ICsvProvider _csv;
        private readonly ITransactionEntriesRepository _transactions;
        private readonly ITransactionTypeDescriptorsRepository _transactionTypeDescriptors;

        public TransactionEntryService(ICsvProvider csv, ITransactionEntriesRepository transactions, ITransactionTypeDescriptorsRepository transactionTypeDescriptors)
        {
            _csv = csv;
            _transactions = transactions;
            _transactionTypeDescriptors = transactionTypeDescriptors;
        }

        public async Task<TransactionEntryModel> Create(TransactionEntryModel transaction)
        {
            transaction.Category = null;
            transaction.DateCreated = DateTime.Now;
            transaction.Type = null;
            transaction.User = null;
            //transaction.UserId = _session.UserId; // TODO Get from session context
            
            var entity = transaction.AsEntity();
            var transactionId = await _transactions.Create(entity);
            var createdTransaction = await _transactions.GetById(transactionId);

            return createdTransaction.AsModel();
        }

        public async Task DeleteAll()
        {
            await _transactions.DeleteAll();
        }

        public async Task<IEnumerable<TransactionEntryModel>> GetAll()
        {
            var transactions = await _transactions.GetWhere(_ => true);

            return transactions
                .Select(x => x.AsModel())
                .OrderByDescending(x => x.DateCreated);
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
                    TotalExpenses = sunday.Where(x => x.Type.Description == "Expense").Sum(x => x.Amount),
                    TotalIncome = sunday.Where(x => x.Type.Description == "Income").Sum(x => x.Amount),
                    Transactions = sunday,
                },
                Monday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[1],
                    TotalExpenses = monday.Where(x => x.Type.Description == "Expense").Sum(x => x.Amount),
                    TotalIncome = monday.Where(x => x.Type.Description == "Income").Sum(x => x.Amount),
                    Transactions = monday,
                },
                Tuesday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[2],
                    TotalExpenses = tuesday.Where(x => x.Type.Description == "Expense").Sum(x => x.Amount),
                    TotalIncome = tuesday.Where(x => x.Type.Description == "Income").Sum(x => x.Amount),
                    Transactions = tuesday,
                },
                Wednesday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[3],
                    TotalExpenses = wednesday.Where(x => x.Type.Description == "Expense").Sum(x => x.Amount),
                    TotalIncome = wednesday.Where(x => x.Type.Description == "Income").Sum(x => x.Amount),
                    Transactions = wednesday,
                },
                Thursday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[4],
                    TotalExpenses = thursday.Where(x => x.Type.Description == "Expense").Sum(x => x.Amount),
                    TotalIncome = thursday.Where(x => x.Type.Description == "Income").Sum(x => x.Amount),
                    Transactions = thursday,
                },
                Friday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[5],
                    TotalExpenses = friday.Where(x => x.Type.Description == "Expense").Sum(x => x.Amount),
                    TotalIncome = friday.Where(x => x.Type.Description == "Income").Sum(x => x.Amount),
                    Transactions = friday,
                },
                Saturday = new WeeklyTransactionsEntry
                {
                    Date = datesOfWeek[6],
                    TotalExpenses = saturday.Where(x => x.Type.Description == "Expense").Sum(x => x.Amount),
                    TotalIncome = saturday.Where(x => x.Type.Description == "Income").Sum(x => x.Amount),
                    Transactions = saturday,
                },
            };
        }

        public async Task ImportFromCsv(string csvFileBase64)
        {
            var records = await _csv.Parse<TransactionCsvRow>(csvFileBase64);

            foreach (var row in records)
            {
                var type = await _transactionTypeDescriptors.GetFirst(x => x.Description == row.Type);

                await _transactions.Create(new TransactionEntry
                {
                    Amount = decimal.Parse(row.Amount, NumberStyles.Currency),
                    DateCreated = DateTime.Parse(row.Date),
                    Description = row.Description,
                    TypeId = type.TransactionTypeDescriptorId,
                    UserId = 5, // TODO Get from current user session
                });
            }
        }

        public async Task<IList<TransactionEntryModel>> Search(TransactionEntrySearchParams searchParams)
        {
            var transactions = await _transactions.Search(searchParams.AsQueryParams());
            
            return transactions
                .Select(x => x.AsModel())
                .OrderByDescending(x => x.DateCreated)
                .ToList();
        }
    }
}
