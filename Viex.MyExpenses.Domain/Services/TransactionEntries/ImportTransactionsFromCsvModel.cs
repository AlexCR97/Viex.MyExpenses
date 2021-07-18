using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Domain.Services.TransactionEntries
{
    public class ImportTransactionsFromCsvModel
    {
        public string CsvFileBase64 { get; set; }
    }
}
