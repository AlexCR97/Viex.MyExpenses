namespace Viex.MyExpenses.Domain.Services.Authentication
{
    public class SignInModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string GrantType { get; set; }
    }
}
