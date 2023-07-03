using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Commands;
using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using LinkUpWorld.UsersMicroservice.Application.Helpers;
using LinkUpWorld.UsersMicroservice.Domain.Entities;
using LinkUpWorld.UsersMicroservice.Domain.Repositories;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Handlers
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GetUserDto>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            PasswordHasher passwordHasher = new PasswordHasher();
            var passSalt = passwordHasher.GenerateSalt();

            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Handle = request.Handle,
                PasswordSalt = passSalt,
                PasswordHash = passwordHasher.HashPassword(request.Password, passSalt),
                Bio = request.Bio,
                ProfilePicture = request.ProfilePicture,
                JoinDate = DateTime.UtcNow,
                LastModified = DateTime.UtcNow,
                IsActive = true
            };

            await _userRepository.CreateAsync(newUser);

            var getUserDto = new GetUserDto
            {
                Id = newUser.Id,
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email= newUser.Email,
                Handle=newUser.Handle,
                Bio = newUser.Bio,
                ProfilePicture = newUser.ProfilePicture,
                IsActive = newUser.IsActive,
            };

            return getUserDto;
        }
    }
}
