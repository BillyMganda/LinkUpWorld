namespace LinkUpWorld.UsersMicroservice.Domain.DTOs
{
    public class TokenResponseDto
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiration { get; set; }
    }
}
