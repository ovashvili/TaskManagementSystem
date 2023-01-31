using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.Role.Commands.AddRoleToUser
{
    public class AddRoleToUserCommand : IRequest<CommandResponse<string>>
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
