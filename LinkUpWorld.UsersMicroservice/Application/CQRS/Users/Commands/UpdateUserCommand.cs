using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Commands
{
    public class UpdateUserCommand : IRequest<GetUserDto>
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;        
        public string Bio { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;        
    }
}
