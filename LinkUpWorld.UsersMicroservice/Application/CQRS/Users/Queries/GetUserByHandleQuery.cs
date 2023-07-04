using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Queries
{
    public class GetUserByHandleQuery : IRequest<GetUserDto>
    {
        public string Handle { get; set; } = string.Empty;
    }
}
