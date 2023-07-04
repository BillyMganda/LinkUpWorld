using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Queries;
using LinkUpWorld.UsersMicroservice.Application.Exceptions;
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
            if (request == null)
            {
                throw new CustomValidationException("request is required.");
            }

            try
            {
                var user = await _userRepository.GetByHandleAsync(request.Handle);

                if(user == null)
                {
                    throw new NotFoundException($"User with handle {request.Handle} not found.");
                }

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
            catch (Exception ex)
            {
                throw new CustomException("An error occurred while getting a user.", ex);
            }
        }
    }
}
