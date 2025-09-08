using BlazorApp.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Application.Users.Queries
{
     public class GetUserByIdQuery : IRequest<UserDTO>
     {
          public int Id { get; set; }
     }
}
