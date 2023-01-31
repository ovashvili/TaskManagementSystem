using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.User.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest<CommandResponse<string>>
    {
        public string Id { get; set; }
    }
}
