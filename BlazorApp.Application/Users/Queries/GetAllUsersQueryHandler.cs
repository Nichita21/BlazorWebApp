using BlazorApp.Infrastructure.Data;
using BlazorApp.Shared.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Application.Users.Queries
{
     public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDTO>>
     {
          private readonly AppDbContext _context;

          public GetAllUsersQueryHandler(AppDbContext context)
          {
               _context = context;
          }

          public async Task<IEnumerable<UserDTO>> Handle(GetAllUsersQuery query, CancellationToken cancellationToken)
          {
               var users = await _context.Users
                    .Select(p => new UserDTO
                    {
                         Id = p.Id,
                         Username = p.Username,
                         Password = p.Password,
                         Email = p.Email
                    })
                    .ToListAsync(cancellationToken);

               return users;
          }
     }
}
