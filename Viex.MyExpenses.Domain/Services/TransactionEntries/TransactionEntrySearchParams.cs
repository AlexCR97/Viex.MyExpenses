using System;
using System.Collections.Generic;
using System.Text;
using Viex.MyExpenses.Persistence.Repositores.TransactionEntries;

namespace Viex.MyExpenses.Domain.Services.TransactionEntries
{
    public class TransactionEntrySearchParams
    {
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }

    public static class TransactionEntrySearchParamsMapper
    {
        public static TransactionEntriesQueryParams AsQueryParams(this TransactionEntrySearchParams searchParams)
        {
            return new TransactionEntriesQueryParams
            {
                DateCreatedFrom = searchParams?.FromDate,
                DateCreatedTo = searchParams?.ToDate,
            };
        }
    }
}
