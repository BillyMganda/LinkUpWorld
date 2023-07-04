using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using MediatR;

namespace LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Queries
{
    public class QueryUserQuery : IRequest<IEnumerable<GetUserDto>>
    {
        public string Name { get; set; }
        public QueryUserQuery(string name)
        {
            Name = name;
        }
    }
}
