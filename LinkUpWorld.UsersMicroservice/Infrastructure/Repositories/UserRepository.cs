using LinkUpWorld.UsersMicroservice.Domain.Repositories;
using LinkUpWorld.UsersMicroservice.Infrastructure.Data;

namespace LinkUpWorld.UsersMicroservice.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _userDbContext;
        public UserRepository(UserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }
    }
}
