using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Domain.Providers.Csv
{
    public class TransactionCsvRow
    {
        public string Date { get; set; }
        public string Amount { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
    }
}
