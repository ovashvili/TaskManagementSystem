using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.Task.Commands.DeleteTask
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, CommandResponse<string>>
    {
        private readonly ITaskService _taskService;

        public DeleteTaskCommandHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<CommandResponse<string>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            return await _taskService.DeleteAsync(request.Id);
        }
    }
}
