using LinkUpWorld.UsersMicroservice.Domain.Entities;

namespace LinkUpWorld.UsersMicroservice.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(Guid id);
        Task<User> GetByHandleAsync(string handle);
        Task<IEnumerable<User>> GetAllAsync();
        Task CreateAsync(User entity);
        Task UpdateAsync(User entity);
        Task DeleteAsync(Guid id);
        Task DeactivateAsync(Guid id);
        Task<IEnumerable<User>> QueryByNameAsync(string name);
    }
}
