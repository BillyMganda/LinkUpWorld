using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Queries;
using LinkUpWorld.UsersMicroservice.Domain.Repositories;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Handlers
{
    public class QueryUserQueryHandler : IRequestHandler<QueryUserQuery, IEnumerable<GetUserDto>>
    {
        private readonly IUserRepository _userRepository;
        public QueryUserQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<GetUserDto>> Handle(QueryUserQuery request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.QueryByNameAsync(request.Name);

            var getUsersDto = users.Select(u => new GetUserDto
            { 
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Handle = u.Handle,
                Bio = u.Bio,
                ProfilePicture = u.ProfilePicture,
                IsActive = u.IsActive,
            });

            return getUsersDto;
        }
    }
}
