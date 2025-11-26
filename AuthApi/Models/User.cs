namespace AuthApi.Models
{

    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;
    }
}
