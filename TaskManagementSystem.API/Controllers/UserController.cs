using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using TaskManagementSystem.API.Common;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.User.Commands.AuthenticateUser;
using TaskManagementSystem.Application.User.Commands.DeleteUser;
using TaskManagementSystem.Application.User.Commands.RegisterUser;
using TaskManagementSystem.Application.User.Commands.UpdateUser;
using TaskManagementSystem.Application.User.Queries.GetUserById;
using TaskManagementSystem.Application.User.Queries.GetUsers;

namespace TaskManagementSystem.API.Controllers;

[Route("/user")]
[ApiVersion("1.0")]
public class UserController : ApiControllerBase
{
    public UserController(IMediator mediator) : base(mediator) { }

    /// <summary>
    /// Register user
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <response code="200">Should return command status</response>
    /// <response code="500">Some unhandled server error occurred</response>
    [HttpPost("create")]
    public async Task<ActionResult<CommandResponse<string>>> RegisterUserAsync([FromBody] RegisterUserCommandModel model, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new RegisterUserCommand { Model = model }, cancellationToken));
    }

    /// <summary>
    /// Get user by id
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <response code="200">Should return user</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User does not have access to resource</response>
    /// <response code="500">Some unhandled server error occurred</response>
    [HttpGet("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<CommandResponse<UserDto>>> GetUserByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetUserByIdQuery { Id = id }, cancellationToken));
    }

    /// <summary>
    /// Get list of users
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <response code="200">Should return users</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User does not have access to resource</response>
    /// <response code="500">Some unhandled server error occurred</response>
    [HttpGet]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<CommandResponse<IEnumerable<UserDto>>>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new GetUsersQuery { }, cancellationToken));
    }

    /// <summary>
    /// Update user
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <response code="200">Should return command status</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User does not have access to resource</response>
    /// <response code="500">Some unhandled server error occurred</response>
    [HttpPut("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<CommandResponse<string>>> UpdateUserAsync([FromRoute] string id, [FromBody] UpdateUserCommandModel model, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new UpdateUserCommand { Id = id, Model = model }, cancellationToken));
    }

    /// <summary>
    /// Authenticate user
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <response code="200">Should return authenticated user</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User does not have access to resource</response>
    /// <response code="500">Some unhandled server error occurred</response>
    [HttpPost("authenticate")]
    public async Task<ActionResult<CommandResponse<AuthenticateUserResponse>>> AuthenticateUserAsync([FromBody] AuthenticateUserCommandModel model, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new AuthenticateUserCommand { Model = model }, cancellationToken));
    }

    /// <summary>
    /// Delete user
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <response code="200">Should return command status</response>
    /// <response code="401">User is not authenticated</response>
    /// <response code="403">User does not have access to resource</response>
    /// <response code="500">Some unhandled server error occurred</response>
    [HttpDelete("{id}")]
    [Authorize(Policy = "Admin")]
    public async Task<ActionResult<CommandResponse<string>>> DeleteUserAsync([FromRoute] string id, CancellationToken cancellationToken)
    {
        return Ok(await Mediator.Send(new DeleteUserCommand { Id = id }, cancellationToken));
    }
}