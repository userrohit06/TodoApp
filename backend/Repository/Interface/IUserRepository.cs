using backend.Models;

namespace backend.Repository.Interface
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<int> SaveChangesAsync();
    }
}
