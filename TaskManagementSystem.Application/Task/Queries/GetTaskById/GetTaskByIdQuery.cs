using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.Task.Queries.GetTaskById
{
    public class GetTaskByIdQuery : IRequest<CommandResponse<TaskDto>>
    {
        public string Id { get; set; }
    }
}
