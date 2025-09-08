using BlazorApp.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Application.Users.Commands
{
     public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, bool>
     {
          private readonly AppDbContext _context;

          public UpdateUserCommandHandler(AppDbContext context)
          {
               _context = context;
          }

          public async Task<bool> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
          {
               var user = await _context.Users.FindAsync(command.User.Id);

               if (user == null)
                    return false;
               user.Username = command.User.Username;
               user.Password = command.User.Password;
               user.Email = command.User.Email;

               _context.Entry(user).State = EntityState.Modified;
               await _context.SaveChangesAsync(cancellationToken);
               
               return true;

          }
     }
}
