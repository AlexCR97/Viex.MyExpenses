using Viex.MyExpenses.Domain.Services;
using Viex.MyExpenses.Persistence.Repositores.Users;

namespace Viex.MyExpenses.Domain.Models
{
    public class UserModel : BaseModel
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
    }

    public static class UserModelMapper
    {
        public static User AsEntity(this UserModel a)
        {
            return new User
            {
                DateCreated = a.DateCreated,
                DateUpdated = a.DateUpdated,
                Email = a.Email,
                FirstName = a.FirstName,
                LastName = a.LastName,
                UserId = a.UserId,
                UserName = a.UserName,
            };
        }

        public static UserModel AsModel(this User a)
        {
            return new UserModel
            {
                DateCreated = a.DateCreated,
                DateUpdated = a.DateUpdated,
                Email = a.Email,
                FirstName = a.FirstName,
                LastName = a.LastName,
                UserId = a.UserId,
                UserName = a.UserName,
            };
        }
    }
}
