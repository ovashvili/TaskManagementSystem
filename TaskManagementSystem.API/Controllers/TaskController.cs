using BrunoZell.ModelBinding;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.API.Common;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Task.Commands.CreateTask;
using TaskManagementSystem.Application.Task.Commands.DeleteTask;
using TaskManagementSystem.Application.Task.Commands.UpdateTask;
using TaskManagementSystem.Application.Task.Queries.GetTaskById;
using TaskManagementSystem.Application.Task.Queries.GetTasks;
using TaskManagementSystem.Application.User.Queries.GetUserById;
using TaskManagementSystem.Application.User.Queries.GetUsers;

namespace TaskManagementSystem.API.Controllers
{
    [Route("/task")]
    [ApiVersion("1.0")]
    public class TaskController : ApiControllerBase
    {
        public TaskController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Create task
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPost("create")]
        [Authorize(Roles = "Admin, Task_Create")]
        public async Task<ActionResult<CommandResponse<string>>> CreateTaskAsync([FromForm] CreateTaskCommandModel model, CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(new CreateTaskCommand { Model = model }, cancellationToken));
        }

        /// <summary>
        /// Update task
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Task_Update")]
        public async Task<ActionResult<CommandResponse<string>>> UpdateTaskAsync(string id, [FromForm] UpdateTaskCommandModel model, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new UpdateTaskCommand { Id = id, Model = model }, cancellationToken));
        }

        /// <summary>
        /// Get task by id
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return task</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Basic, Task_View")]
        public async Task<ActionResult<CommandResponse<TaskDto>>> GetTaskByIdAsync([FromRoute] string id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetTaskByIdQuery { Id = id }, cancellationToken));
        }

        /// <summary>
        /// Get list of tasks
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return tasks</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpGet]
        [Authorize(Roles = "Admin, Basic, Task_View")]
        public async Task<ActionResult<CommandResponse<IEnumerable<TaskDto>>>> GetTasksAsync(CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new GetTasksQuery { }, cancellationToken));
        }

        /// <summary>
        /// Delete task
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <response code="200">Should return command status</response>
        /// <response code="401">User is not authenticated</response>
        /// <response code="403">User does not have access to resource</response>
        /// <response code="500">Some unhandled server error occurred</response>
        [HttpDelete("{id}")]
        [Authorize(Roles  = "Admin, Task_Delete")]
        public async Task<ActionResult<CommandResponse<string>>> DeleteTaskAsync(string id, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(new DeleteTaskCommand { Id = id }, cancellationToken));
        }
    }
}
