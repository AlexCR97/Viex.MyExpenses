using System;
using System.Collections.Generic;
using System.Text;
using Viex.MyExpenses.Domain.Models;

namespace Viex.MyExpenses.Domain.Services.TransactionEntries
{
    public class WeeklyTransactionsEntry
    {
        public DateTime Date { get; set; }
        public decimal TotalExpenses { get; set; }
        public decimal TotalIncome { get; set; }
        public IList<TransactionEntryModel> Transactions { get; set; }
    }
}
