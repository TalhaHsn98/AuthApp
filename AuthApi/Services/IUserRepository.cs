using AuthApi.Models;

namespace AuthApi.Services
{
    public interface IUserRepository
    {
        Task<User?> GetByUserNameAsync(string userName);
        Task AddAsync(User user);
    }
}
