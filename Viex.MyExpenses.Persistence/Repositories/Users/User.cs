using System.ComponentModel.DataAnnotations.Schema;
using Viex.MyExpenses.Persistence.Repositories.RoleDescriptors;

namespace Viex.MyExpenses.Persistence.Repositories.Users
{
    public class User : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public int RoleDescriptorId { get; set; }
        public RoleDescriptor RoleDescriptor { get; set; }
    }
}
