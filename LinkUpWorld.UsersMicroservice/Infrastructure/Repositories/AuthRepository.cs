using LinkUpWorld.UsersMicroservice.Application.Exceptions;
using LinkUpWorld.UsersMicroservice.Application.Helpers;
using LinkUpWorld.UsersMicroservice.Domain.DTOs;
using LinkUpWorld.UsersMicroservice.Domain.Entities;
using LinkUpWorld.UsersMicroservice.Domain.Repositories;

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
            await _userRepository.UpdateAsync(user);

            // Create response DTO
            var response = new AuthResponseDto
            {
                UserId = user.Id.ToString(),
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = _jwtService.GetAccessTokenExpiration()
            };

            return response;
        }
        public async Task<TokenResponseDto> RefreshToken(RefreshTokenRequestDto request)
        {
            // Retrieve the refresh token from the request
            string refreshToken = request.RefreshToken;

            // Validate the refresh token
            if (!await _jwtService.ValidateRefreshToken(refreshToken))
            {
                throw new CustomValidationException("Invalid refresh token.");
            }

            // Extract the user ID from the refresh token
            Guid userId = await _jwtService.GetUserIdFromRefreshToken(refreshToken);

            // Check if the user exists
            User user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            // Generate a new access token and refresh token for the user
            string newAccessToken = _jwtService.GenerateAccessToken(userId, user);
            string newRefreshToken = _jwtService.GenerateRefreshToken();

            // Update the refresh token in the database
            await _userRepository.UpdateRefreshToken(userId, newRefreshToken);

            // Return the new access token and refresh token
            return new TokenResponseDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
        }
        public async Task<LogoutResponseDto> Logout(LogoutRequestDto request)
        {
            // Retrieve the refresh token from the request
            string refreshToken = request.RefreshToken;

            // Validate the refresh token
            if (!await _jwtService.ValidateRefreshToken(refreshToken))
            {
                throw new CustomValidationException("Invalid refresh token.");
            }

            // Extract the user ID from the refresh token
            Guid userId = await _jwtService.GetUserIdFromRefreshToken(refreshToken);

            // Delete the refresh token from the database
            await _userRepository.DeleteRefreshToken(userId);

            // Return a successful logout response
            return new LogoutResponseDto { Message = "Logout successful." };
        }
        public async Task<TokenResponseDto> ForgotPassword(ForgotPasswordRequestDto request)
        {
            // Retrieve the email address from the request
            string email = request.Email;

            // Check if a user with the provided email exists in the database
            User user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            // Generate a password reset token for the user
            string resetToken = _jwtService.GeneratePasswordResetToken(user);

            // Save the password reset token in the user's record
            user.ResetPasswordToken = resetToken;
            user.ResetPasswordTokenExpiry = DateTime.UtcNow.AddHours(24); // Set an expiry time for the reset token
            await _userRepository.UpdateAsync(user);

            // Send a password reset email to the user's email address
            _emailService.SendPasswordResetEmail(user.Email, resetToken);

            // Return the password reset token
            return new TokenResponseDto { Token = resetToken };
        }
    }
}
