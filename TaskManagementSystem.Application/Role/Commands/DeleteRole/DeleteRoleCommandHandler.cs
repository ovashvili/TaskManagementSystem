using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.Role.Commands.DeleteRole
{
    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, CommandResponse<string>>
    {
        private readonly IRoleManagerService _roleManagerService;

        public DeleteRoleCommandHandler(IRoleManagerService roleManagerService)
        {
            _roleManagerService = roleManagerService;
        }

        public async Task<CommandResponse<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleManagerService.DeleteAsync(request.RoleName);
        }
    }
}
