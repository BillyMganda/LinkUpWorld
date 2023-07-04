﻿using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Commands;
using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using LinkUpWorld.UsersMicroservice.Application.Helpers;
using LinkUpWorld.UsersMicroservice.Domain.Entities;
using LinkUpWorld.UsersMicroservice.Domain.Repositories;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
            // Password salt and hash
            PasswordHasher passwordHasher = new PasswordHasher();
            var passSalt = passwordHasher.GenerateSalt();

            // convert image to string
            string profilePictureString = null!;

            if (request.ProfilePictureFile != null && request.ProfilePictureFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await request.ProfilePictureFile.CopyToAsync(memoryStream);
                    profilePictureString = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

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
                ProfilePicture = profilePictureString!,
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