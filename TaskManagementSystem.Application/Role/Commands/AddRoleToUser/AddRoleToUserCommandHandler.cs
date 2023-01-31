using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.Role.Commands.AddRoleToUser
{
    public class AddRoleToUserCommandHandler : IRequestHandler<AddRoleToUserCommand, CommandResponse<string>>
    {
        private readonly IRoleManagerService _roleManagerService;

        public AddRoleToUserCommandHandler(IRoleManagerService roleManagerService)
        {
            _roleManagerService = roleManagerService;
        }

        public Task<CommandResponse<string>> Handle(AddRoleToUserCommand request, CancellationToken cancellationToken)
        {
            return _roleManagerService.AddRoleToUserAsync(request.UserId, request.RoleName);
        }
    }
}
