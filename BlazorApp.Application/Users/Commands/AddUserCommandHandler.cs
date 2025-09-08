using BlazorApp.Domain.Entities;
using BlazorApp.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Application.Users.Commands
{
     public class AddUserCommandHandler : IRequestHandler<AddUserCommand, int>
     {
          private readonly AppDbContext _context;

          public AddUserCommandHandler(AppDbContext context)
          { 
               _context = context;
          }

          public async Task<int> Handle(AddUserCommand command, CancellationToken cancellationToken)
          {
               var user = new User
               {
                    Username = command.User.Username,
                    Password = command.User.Password,
                    Email = command.User.Email
               };

               _context.Users.Add(user);
               await _context.SaveChangesAsync(cancellationToken);

               return user.Id;
          }
     }
}
