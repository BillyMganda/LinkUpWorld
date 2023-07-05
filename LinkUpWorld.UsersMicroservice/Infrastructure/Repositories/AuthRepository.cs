using LinkUpWorld.UsersMicroservice.Domain.Repositories;

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
