using BlazorApp.Application.Users.Commands;
using BlazorApp.Application.Users.Queries;
using BlazorApp.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlazorApp.API.Controllers
{
     [ApiController]
     [Route("api/[controller]")]
     public class UsersController : ControllerBase
     {
          private readonly IMediator _mediator;

          public UsersController(IMediator mediator)
          {
               _mediator = mediator;
          }

          // GET: api/users
          [HttpGet]
          public async Task<ActionResult<List<UserDTO>>> GetAllUsers()
          {
               var users = await _mediator.Send(new GetAllUsersQuery());
               return Ok(users);
          }

          // GET: api/users/{id}
          [HttpGet("{id}")]
          public async Task<ActionResult<UserDTO>> GetUser(int id)
          {
               var user = await _mediator.Send(new GetUserByIdQuery(id));
               if (user == null) return NotFound();
               return Ok(user);
          }

          // POST: api/users
          [HttpPost]
          public async Task<ActionResult<UserDTO>> AddUser([FromBody] UserDTO user)
          {
               var newUser = await _mediator.Send(new AddUserCommand(user));
               return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
          }

          // PUT: api/users/{id}
          [HttpPut("{id}")]
          public async Task<ActionResult<UserDTO>> UpdateUser(int id, [FromBody] UserDTO user)
          {
               if (id != user.Id) return BadRequest("User ID mismatch");

               var updatedUser = await _mediator.Send(new UpdateUserCommand(user));
               if (updatedUser == null) return NotFound();

               return Ok(updatedUser);
          }

          // DELETE: api/users/{id}
          [HttpDelete("{id}")]
          public async Task<IActionResult> DeleteUser(int id)
          {
               await _mediator.Send(new DeleteUserCommand(id));
               return NoContent();
          }
     }
}
