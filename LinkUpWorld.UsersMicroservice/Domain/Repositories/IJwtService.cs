namespace LinkUpWorld.UsersMicroservice.Domain.Repositories
{
    public interface IJwtService
    {
        string GenerateAccessToken(Guid userId, string userEmail);
        string GenerateRefreshToken();
        DateTime GetAccessTokenExpiration();
    }
}
