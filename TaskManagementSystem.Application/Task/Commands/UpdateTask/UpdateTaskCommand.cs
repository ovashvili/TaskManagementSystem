using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.Task.Commands.UpdateTask
{
    public class UpdateTaskCommand : IRequest<CommandResponse<TaskDto>>
    {
        public string Id { get; set; }
        public UpdateTaskCommandModel Model { get; set; }
    }

    public class UpdateTaskCommandModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AdditionalDescription { get; set; }
        public string AssignedTo { get; set; }
        public List<IFormFile> AttachedFiles { get; set; }
    }
}
