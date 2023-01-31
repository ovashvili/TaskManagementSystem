using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.Contracts
{
    public interface IRoleManagerService
    {
        Task<CommandResponse<string>> AddAsync(string roleName);
        Task<CommandResponse<IEnumerable<string>>> GetUserRolesAsync(string userId);
        Task<CommandResponse<string>> AddRoleToUserAsync(string userId, string roleName);
        Task<CommandResponse<string>> RemoveRoleFromUserAsync(string userId, string roleName);
        Task<CommandResponse<IEnumerable<RoleDto>>> GetRolesAsync();
        Task<CommandResponse<string>> DeleteAsync(string roleName);
    }
}
