using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.Role.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserQuery : IRequest<CommandResponse<string>>
    {
        public string RoleName { get; set; }
        public string UserId { get; set; }
    }
}
