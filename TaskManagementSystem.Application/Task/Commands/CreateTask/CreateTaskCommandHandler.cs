using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.Task.Commands.CreateTask
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CommandResponse<TaskDto>>
    {
        private readonly ITaskService _taskService;

        public CreateTaskCommandHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<CommandResponse<TaskDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            return await _taskService.CreateAsync(request.Model);
        }
    }
}
