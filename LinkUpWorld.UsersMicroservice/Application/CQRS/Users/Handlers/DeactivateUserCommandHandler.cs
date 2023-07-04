using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Commands;
using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using LinkUpWorld.UsersMicroservice.Application.Exceptions;
using LinkUpWorld.UsersMicroservice.Domain.Repositories;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Handlers
{
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, GetUserDto>
    {
        private readonly IUserRepository _userRepository;
        public DeactivateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user != null)
                {
                    await _userRepository.DeactivateAsync(request.Id);
                }

                var getUserDto = new GetUserDto
                {
                    Id = user!.Id,
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
                throw new CustomException("An error occurred while deactivating a user.", ex);
            }
        }
    }
}
