using BlazorApp.Application.Users.Commands;
using BlazorApp.Application.Users.Queries;
using BlazorApp.Domain.Entities;
using BlazorApp.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
     public class UserController : ControllerBase
     {
          private readonly IMediator _mediator;

          public UserController(IMediator mediator)
          {
               _mediator = mediator;
          }

          [HttpGet]
          public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUsers()
          {
               var result = await _mediator.Send(new GetAllUsersQuery());
               return Ok(result);
          }

          [HttpGet("{id}")]
          public async Task<ActionResult<UserDTO>> GetUser(int id)
          {
               var result = await _mediator.Send(new GetUserByIdQuery { Id = id });

               if (result == null)
                    return NotFound();

               return Ok(result);
          }

          [HttpPost]
          public async Task<ActionResult<UserDTO>> AddUser(UserDTO userDTO)
          {
               var command = new AddUserCommand(userDTO);
               var userId = await _mediator.Send(command);

               var addedUser = await _mediator.Send(new GetUserByIdQuery { Id = userId });
               return CreatedAtAction(nameof(GetUser), new { id = userId }, addedUser);
          }

          [HttpPut("{id}")]
          public async Task<IActionResult> PutUser(int id, UserDTO userDTO)
          {
               if (id != userDTO.Id)
                    return BadRequest();

               var command = new UpdateUserCommand(userDTO);
               var result = await _mediator.Send(command);

               if (!result)
                    return NotFound();

               return NoContent();
          }

          [HttpDelete("{id}")]
          public async Task<IActionResult> DeleteUser(int id)
          {
               var command = new DeleteUserCommand { Id = id };
               var result = await _mediator.Send(command);

               if (!result)
                    return NotFound();

               return NoContent();
          }
     }
}

