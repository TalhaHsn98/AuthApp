using AuthApi.Models;

namespace AuthApi.Services
{

    public class InMemoryUserRepository : IUserRepository
    {
        private static readonly List<User> _users = new();

        public Task<User?> GetByUserNameAsync(string userName)
        {
            var user = _users
                .FirstOrDefault(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(user);
        }

        public Task AddAsync(User user)
        {
            _users.Add(user);
            return Task.CompletedTask;
        }
    }
}
