using MediatR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.Task.Queries.GetTasks
{
    public class GetTasksQueryHandler : IRequestHandler<GetTasksQuery, CommandResponse<IEnumerable<TaskDto>>>
    {
        private readonly ITaskService _taskService;

        public GetTasksQueryHandler(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public async Task<CommandResponse<IEnumerable<TaskDto>>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
        {
            return await _taskService.GetAllAsync();
        }
    }
}
