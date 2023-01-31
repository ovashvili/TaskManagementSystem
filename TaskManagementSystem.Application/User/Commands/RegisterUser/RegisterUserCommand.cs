using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.User.Commands.RegisterUser
{
    public class RegisterUserCommand : IRequest<CommandResponse<UserDto>>
    {
        public RegisterUserCommandModel Model { get; set; }
    }

    public class RegisterUserCommandModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
