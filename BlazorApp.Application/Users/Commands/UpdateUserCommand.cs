using BlazorApp.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp.Application.Users.Commands
{
     public class UpdateUserCommand : IRequest<bool>
     {
          public UserDTO User { get; set; } = new();

          public UpdateUserCommand(UserDTO user) 
          {
               User = user;
          }

          public UpdateUserCommand() { }
     }
}
