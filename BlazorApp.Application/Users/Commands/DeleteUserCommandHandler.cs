using BlazorApp.Infrastructure.Data;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Application.Users.Commands
{
     public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
     {
          private readonly AppDbContext _context;

          public DeleteUserCommandHandler(AppDbContext context) 
          {
               _context = context;
          }

          public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
          {
               var user = await _context.Users.FindAsync(command.Id);

               if (user is not null)
               {
                    _context.Users.Remove(user);
                    await _context.SaveChangesAsync(cancellationToken);

               }     
               
               

          }
     }
}
