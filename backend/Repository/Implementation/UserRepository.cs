using backend.Models;
using backend.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace backend.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoDbContext _dbContext;

        public UserRepository(TodoDbContext dbContext) => this._dbContext = dbContext;

        public async Task AddUserAsync(User user)
        {
            await this._dbContext.Users.AddAsync(user);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await this._dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {
            return await this._dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await this._dbContext.SaveChangesAsync();
        }
    }
}
