using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Queries;
using LinkUpWorld.UsersMicroservice.Domain.Repositories;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Handlers
{
    public class GetUserByHandleQueryHandler : IRequestHandler<GetUserByHandleQuery, GetUserDto>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByHandleQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(GetUserByHandleQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByHandleAsync(request.Handle);

            var getUserDto = new GetUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Handle = user.Handle,
                Bio = user.Bio,
                ProfilePicture = user.ProfilePicture,
                IsActive = user.IsActive,
            };

            return getUserDto;
        }
    }
}
