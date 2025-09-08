using BlazorApp.Shared.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


 namespace BlazorApp.Application.Users.Commands
 {
       public class AddUserCommand : IRequest<UserDTO>  // <-- schimbat aici
       {
               public UserDTO User { get; set; } = new();

               public AddUserCommand(UserDTO user)
               {
                    User = user;
               }

               public AddUserCommand() { }
       }
 }


