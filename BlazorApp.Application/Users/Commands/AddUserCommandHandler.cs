using BlazorApp.Application.Users.Commands;
using BlazorApp.Domain.Entities;
using BlazorApp.Infrastructure.Data;
using BlazorApp.Shared.DTOs;
using MediatR;

namespace BlazorApp.Application.Users.Commands
{
     public class AddUserCommandHandler : IRequestHandler<AddUserCommand, UserDTO>
     {
          private readonly AppDbContext _context;

          public AddUserCommandHandler(AppDbContext context)
          {
               _context = context;
          }

          public async Task<UserDTO> Handle(AddUserCommand command, CancellationToken cancellationToken)
          {
               var user = new User
               {
                    Username = command.User.Username,
                    Password = command.User.Password, // Hashed dacă vrei
                    Email = command.User.Email
               };

               _context.Users.Add(user);
               await _context.SaveChangesAsync(cancellationToken);

               // Returnăm UserDTO complet, astfel Blazor poate să-l folosească
               return new UserDTO
               {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Password = user.Password
               };
          }
     }
}
