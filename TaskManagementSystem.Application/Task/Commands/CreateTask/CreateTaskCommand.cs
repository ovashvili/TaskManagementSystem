using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace TaskManagementSystem.Application.Task.Commands.CreateTask
{
    public class CreateTaskCommand : IRequest<CommandResponse<TaskDto>>
    {
        public CreateTaskCommandModel Model { get; set; }
        public List<IFormFile> AttachedFiles { get; set; }
    }

    public class CreateTaskCommandModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string AdditionalDescription { get; set; }
        public string AssignedTo { get; set; }
        public List<IFormFile> AttachedFiles { get; set; }
    }
}
