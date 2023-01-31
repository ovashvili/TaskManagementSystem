using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.Task.Commands.UpdateTask
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, CommandResponse<TaskDto>>
    {
        private readonly ITaskService _taskService;

        public UpdateTaskCommandHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<CommandResponse<TaskDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            return await _taskService.UpdateAsync(request.Id, request.Model);
        }
    }
}
