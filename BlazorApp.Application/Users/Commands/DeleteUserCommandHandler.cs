using BlazorApp.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Application.Users.Commands
{
     public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, bool>
     {
          private readonly AppDbContext _context;

          public DeleteUserCommandHandler(AppDbContext context) 
          {
               _context = context;
          }

          public async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
          {
               var user = await _context.Users.FindAsync(command.Id);

               if (user == null)
                    return false;
               _context.Users.Remove(user);
               await _context.SaveChangesAsync(cancellationToken);
               return true;

          }
     }
}
