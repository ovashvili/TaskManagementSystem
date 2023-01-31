using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using TaskManagementSystem.API.Common;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Role.Commands.AddRoleToUser;
using TaskManagementSystem.Application.Role.Commands.CreateRole;
using TaskManagementSystem.Application.Role.Commands.DeleteRole;
using TaskManagementSystem.Application.Role.Commands.RemoveRoleFromUser;
using TaskManagementSystem.Application.Role.Queries.GetRoles;
using TaskManagementSystem.Application.Role.Queries.GetUserRoles;
using TaskManagementSystem.Application.User.Queries.GetUsers;

namespace TaskManagementSystem.API.Controllers
{
    [Route("/role")]
    [ApiVersion("1.0")]
    [Authorize(Policy = "Admin")]
    public class RoleController : ApiControllerBase
    {
        public RoleController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Create role
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPost("create")]
        public async Task<ActionResult<CommandResponse<string>>> AddRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new CreateRoleCommand { RoleName = roleName }, cancellationToken));
        }

        /// <summary>
        /// Add role to user
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPost("add-to-user")]
        public async Task<ActionResult<CommandResponse<string>>> AddRoleToUserAsync(string userId, string roleName, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new AddRoleToUserCommand { UserId = userId, RoleName = roleName }, cancellationToken));
        }

        /// <summary>
        /// Get list of roles
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return roles</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpGet("get-roles")]
        public async Task<ActionResult<CommandResponse<IEnumerable<RoleDto>>>> GetRolesAsync(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetRolesQuery { }, cancellationToken));
        }

        /// <summary>
        /// Get user's roles
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return user's roles</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpGet("get-user-roles")]
        public async Task<ActionResult<CommandResponse<IEnumerable<RoleDto>>>> GetUserRolesAsync(string userId, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetUserRolesQuery { UserId = userId }, cancellationToken));
        }

        /// <summary>
        /// Delete role
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpDelete("{roleName}")]
        public async Task<ActionResult<CommandResponse<string>>> DeleteRoleAsync(string role, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new DeleteRoleCommand { RoleName = role }, cancellationToken));
        }

        /// <summary>
        /// Remove role from user
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpDelete("{roleName}/{userId}")]
        public async Task<ActionResult<CommandResponse<string>>> RemoveRoleFromUserAsync(string roleName, string userId, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new RemoveRoleFromUserQuery { RoleName = roleName, UserId = userId }, cancellationToken));
        }
    }
}
