using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Commands
{
    public class DeleteUserCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
