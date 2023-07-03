using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Queries
{
    public class GetAllUsersQuery : IRequest<IEnumerable<GetUserDto>>
    {
    }
}
