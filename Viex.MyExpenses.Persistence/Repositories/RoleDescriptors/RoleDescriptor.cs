using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Viex.MyExpenses.Persistence.Repositories.RoleDescriptors
{
    public class RoleDescriptor : BaseDescriptorEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleDescriptorId { get; set; }
    }
}
