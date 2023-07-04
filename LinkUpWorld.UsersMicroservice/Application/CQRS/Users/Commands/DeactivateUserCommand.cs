using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Commands
{
    public class DeactivateUserCommand : IRequest<GetUserDto>
    {
        public Guid Id { get; set; }
    }
}
