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
               var user = await _context.Users.FindAsync(new object[] { command.User.Id }, cancellationToken);

               if (user == null)
                    return false;

               // Update user properties
               user.Username = command.User.Username;
               user.Email = command.User.Email;

               // Only update password if it's provided (not null or empty)
               if (!string.IsNullOrWhiteSpace(command.User.Password))
               {
                    user.Password = command.User.Password; // In production, hash this!
               }

               _context.Entry(user).State = EntityState.Modified;

               try
               {
                    await _context.SaveChangesAsync(cancellationToken);
                    return true;
               }
               catch (DbUpdateException)
               {
                    // Handle potential database errors (e.g., unique constraints)
                    return false;
               }
          }
     }
}
