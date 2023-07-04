using LinkUpWorld.UsersMicroservice.Domain.DTOs;

namespace LinkUpWorld.UsersMicroservice.Domain.Repositories
{
    public interface IAuthRepository
    {        
        Task<AuthResponseDto> Login(LoginRequestDto request);
        Task<TokenResponseDto> RefreshToken(RefreshTokenRequestDto request);
        Task<LogoutResponseDto> Logout(LogoutRequestDto request);
        Task<TokenResponseDto> ForgotPassword(ForgotPasswordRequestDto request);
        Task<ResetPasswordResponseDto> ResetPassword(ResetPasswordRequestDto request);
    }
}
