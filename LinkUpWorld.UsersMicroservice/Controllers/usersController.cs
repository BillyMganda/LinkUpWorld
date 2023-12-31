﻿using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Commands;
using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.DTOs;
using LinkUpWorld.UsersMicroservice.Application.CQRS.Users.Queries;
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
        public async Task<ActionResult<GetUserDto>> CreateUser([FromForm] CreateUserCommand command)
        {
            var result = await _mediator.Send(command);
            
            return Created("", result);
        }
        
        [HttpGet]        
        public async Task<ActionResult<List<GetUserDto>>> GetAllUsers()
        {
            var query = new GetAllUsersQuery();
            var getUserDtos = await _mediator.Send(query);

            return Ok(getUserDtos);
        }

        [HttpGet("id/{id}")]
        public async Task<ActionResult<GetUserDto>> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery { Id = id };
            var user = await _mediator.Send(query);

            return Ok(user);
        }

        [HttpGet("handle/{handle}")]
        public async Task<ActionResult<GetUserDto>> GetUserByHandle(string handle)
        {
            var query = new GetUserByHandleQuery { Handle = handle };
            var user = await _mediator.Send(query);

            return Ok(user);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserCommand command)
        {
            var updatedUser = await _mediator.Send(command);

            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand { Id = id };
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("deactivate")]
        public async Task<IActionResult> DeactivateUser([FromBody] DeactivateUserCommand command)
        {
            var updatedUser = await _mediator.Send(command);

            return Ok(updatedUser);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers(string name)
        {
            var query = new QueryUserQuery(name);
            var users = await _mediator.Send(query);

            return Ok(users);
        }
    }
}
