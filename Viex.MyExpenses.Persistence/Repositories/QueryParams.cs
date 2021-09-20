using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Persistence.Repositories
{
    public class QueryParams
    {
        public DateTime? DateCreatedFrom { get; set; }
        public DateTime? DateCreatedTo { get; set; }
        public DateTime? DateUpdatedFrom { get; set; }
        public DateTime? DateUpdatedTo { get; set; }
        public string SearchValue { get; set; }
    }
}
