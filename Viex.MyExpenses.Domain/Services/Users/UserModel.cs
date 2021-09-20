using Viex.MyExpenses.Domain.Services;
using Viex.MyExpenses.Persistence.Repositories.Users;

namespace Viex.MyExpenses.Domain.Models
{
    public class UserModel : BaseModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }

        public string RoleDescriptor { get; set; }
    }
}
