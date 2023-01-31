using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.User.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest<CommandResponse<UserDto>>
    {
        public string Id { get; set; }
        public UpdateUserCommandModel Model { get; set; }
    }

    public class UpdateUserCommandModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
