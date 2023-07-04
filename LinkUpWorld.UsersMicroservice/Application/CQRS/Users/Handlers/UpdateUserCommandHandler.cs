using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Commands;
using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using LinkUpWorld.UsersMicroservice.Application.Exceptions;
using LinkUpWorld.UsersMicroservice.Domain.Repositories;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, GetUserDto>
    {
        private readonly IUserRepository _userRepository;
        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new CustomValidationException("request is required.");
            }

            try
            {
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user == null)
                {
                    throw new NotFoundException($"User with id {request.Id} not found.");
                }

                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Bio = request.Bio;
                user.ProfilePicture = request.ProfilePicture;
                user.LastModified = DateTime.UtcNow;

                await _userRepository.UpdateAsync(user);

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
                throw new CustomException("An error occurred.", ex);
            }
        }
    }
}
