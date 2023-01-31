using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.Role.Commands.RemoveRoleFromUser
{
    public class RemoveRoleFromUserQueryHandler : IRequestHandler<RemoveRoleFromUserQuery, CommandResponse<string>>
    {
        private readonly IRoleManagerService _roleManagerService;

        public RemoveRoleFromUserQueryHandler(IRoleManagerService roleManagerService)
        {
            _roleManagerService = roleManagerService;
        }

        public async Task<CommandResponse<string>> Handle(RemoveRoleFromUserQuery request, CancellationToken cancellationToken)
        {
            return await _roleManagerService.RemoveRoleFromUserAsync(request.UserId, request.RoleName);
        }
    }
}
