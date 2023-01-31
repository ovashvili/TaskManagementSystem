using System;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Task.Commands.CreateTask;
using TaskManagementSystem.Application.Task.Commands.UpdateTask;

namespace TaskManagementSystem.Application.Contracts
{
    public interface ITaskService
    {
        Task<CommandResponse<TaskDto>> CreateAsync(CreateTaskCommandModel model);
        Task<CommandResponse<TaskDto>> GetByIdAsync(string id);
        Task<CommandResponse<IEnumerable<TaskDto>>> GetAllAsync();
        Task<CommandResponse<TaskDto>> UpdateAsync(string id, UpdateTaskCommandModel model);
        Task<CommandResponse<string>> DeleteAsync(string id);
    }
}
