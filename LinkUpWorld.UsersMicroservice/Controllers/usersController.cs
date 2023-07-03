using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Commands;
using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LinkUpWorld.UsersMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public usersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<GetUserDto>> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);

            return Created("", result);
        }
    }
}
