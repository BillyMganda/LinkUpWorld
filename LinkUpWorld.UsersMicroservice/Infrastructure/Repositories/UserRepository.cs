using LinkUpWorld.UsersMicroservice.Domain.Entities;
using LinkUpWorld.UsersMicroservice.Domain.Repositories;
using LinkUpWorld.UsersMicroservice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LinkUpWorld.UsersMicroservice.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        public UserRepository(UserDbContext userDbContext)
        {
            _dbContext = userDbContext;
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var results = await _dbContext.Set<User>().FindAsync(id);  
            return results!;
        }

        public async Task<User> GetByHandleAsync(string handle)
        {
            var results = await _dbContext.Set<User>().FindAsync(handle);
            return results!;
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var results = await _dbContext.Set<User>().ToListAsync();
            return results;
        }

        public async Task CreateAsync(User entity)
        {
            await _dbContext.Set<User>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(User entity)
        {
            _dbContext.Set<User>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _dbContext.Set<User>().FindAsync(id);
            if (user != null)
            {
                _dbContext.Set<User>().Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeactivateAsync(Guid id)
        {
            var user = await _dbContext.Set<User>().FindAsync(id);
            if (user != null)
            {
                user.IsActive = false;
                user.LastModified = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<User>> QueryByNameAsync(string name)
        {
            var users = await _dbContext.Set<User>()
                .Where(u => u.FirstName.Contains(name) || u.LastName.Contains(name))
                .ToListAsync();

            return users;
        }
    }
}
