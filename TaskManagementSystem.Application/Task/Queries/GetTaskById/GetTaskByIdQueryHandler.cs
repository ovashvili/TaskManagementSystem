using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.Task.Queries.GetTaskById
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, CommandResponse<TaskDto>>
    {
        private readonly ITaskService _taskService;

        public GetTaskByIdQueryHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<CommandResponse<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            return await _taskService.GetByIdAsync(request.Id);
        }
    }
}
