using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.Role.Queries.GetUserRoles
{
    public class GetUserRolesQuery : IRequest<CommandResponse<IEnumerable<string>>>
    {
        public string UserId { get; set; }
    }
}
