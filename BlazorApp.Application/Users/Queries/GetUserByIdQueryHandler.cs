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
     public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserDTO>
     {
          private readonly AppDbContext _context;

          public GetUserByIdQueryHandler(AppDbContext context)
          {
               _context = context;
          }

          public async Task<UserDTO?> Handle(GetUserByIdQuery query, CancellationToken cancellationToken)
          {
               var user = await _context.Users
                    .Where(p => p.Id == query.Id)
                    .Select(p => new UserDTO
                    {
                         Id = p.Id,
                         Username = p.Username,
                         Password = p.Password,
                         Email = p.Email,
                    })
                    .FirstOrDefaultAsync(cancellationToken);
               return user;
          }
     }
}
