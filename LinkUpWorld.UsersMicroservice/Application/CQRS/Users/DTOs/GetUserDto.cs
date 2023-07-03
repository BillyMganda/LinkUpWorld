namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs
{
    public class GetUserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Handle { get; set; } = string.Empty;        
        public string Bio { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}
