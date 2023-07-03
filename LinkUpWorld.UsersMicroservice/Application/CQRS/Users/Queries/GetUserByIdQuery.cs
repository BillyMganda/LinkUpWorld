using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Queries
{
    public class GetUserByIdQuery : IRequest<GetUserDto>
    {
        public Guid Id { get; set; }
    }
}
