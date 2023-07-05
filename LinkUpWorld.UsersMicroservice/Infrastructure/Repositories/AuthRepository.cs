using LinkUpWorld.UsersMicroservice.Domain.Repositories;
using LinkUpWorld.UsersMicroservice.Infrastructure.Data;

namespace LinkUpWorld.UsersMicroservice.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {        
        private readonly IUserRepository _userRepository;
        public AuthRepository(IUserRepository userRepository)
        {           
            _userRepository = userRepository;
        }
    }
}
