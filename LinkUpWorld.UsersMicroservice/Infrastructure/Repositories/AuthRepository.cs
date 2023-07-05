using LinkUpWorld.UsersMicroservice.Application.Exceptions;
using LinkUpWorld.UsersMicroservice.Application.Helpers;
using LinkUpWorld.UsersMicroservice.Domain.DTOs;
using LinkUpWorld.UsersMicroservice.Domain.Repositories;
using Microsoft.AspNetCore.Identity;

namespace LinkUpWorld.UsersMicroservice.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {        
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        public AuthRepository(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        // Helper method to validate the password
        private bool ValidatePassword(string password, string salt, string hash)
        {
            var passwordHasher = new PasswordHasher();
            var hashedPassword = passwordHasher.HashPassword(password, salt);
            return passwordHasher.VerifyPassword(password, hashedPassword);
        }
        public async Task<AuthResponseDto> Login(LoginRequestDto request)
        {
            // Retrieve user by email
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            // Validate password
            if (!ValidatePassword(request.Password, user.PasswordSalt, user.PasswordHash))
            {
                throw new UnauthorizedException("Invalid email or password.");
            }

            // Generate access token and refresh token
            var accessToken = _jwtService.GenerateAccessToken(user.Id, user.Email);
            var refreshToken = _jwtService.GenerateRefreshToken();

            // Save refresh token to the user entity (e.g., database)
            user.RefreshToken = refreshToken;
            await _userRepository.UpdateAsync(user.Id, user);

            // Create response DTO
            var response = new AuthResponseDto
            {
                UserId = user.Id,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = _jwtService.GetAccessTokenExpiration()
            };

            return response;
        }
    }
}
