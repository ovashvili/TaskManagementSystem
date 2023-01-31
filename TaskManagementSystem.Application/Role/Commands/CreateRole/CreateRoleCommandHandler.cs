using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.Role.Commands.CreateRole
{
    public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CommandResponse<string>>
    {
        private readonly IRoleManagerService _roleManagerService;

        public CreateRoleCommandHandler(IRoleManagerService roleManagerService)
        {
            _roleManagerService = roleManagerService;
        }

        public async Task<CommandResponse<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _roleManagerService.AddAsync(request.RoleName);
        }
    }
}
