using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.Role.Queries.GetRoles
{
    public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, CommandResponse<IEnumerable<RoleDto>>>
    {
        private readonly IRoleManagerService _roleManagerService;

        public GetRolesQueryHandler(IRoleManagerService roleManagerService)
        {
            _roleManagerService = roleManagerService;
        }

        public async Task<CommandResponse<IEnumerable<RoleDto>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            return await _roleManagerService.GetRolesAsync();
        }
    }
}
