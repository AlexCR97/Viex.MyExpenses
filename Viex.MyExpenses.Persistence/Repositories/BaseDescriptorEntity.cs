using System;
using System.Collections.Generic;
using System.Text;

namespace Viex.MyExpenses.Persistence.Repositories
{
    public class BaseDescriptorEntity : BaseEntity
    {
        public string Description { get; set; }
    }
}
