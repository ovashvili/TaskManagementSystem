using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Enums;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;
using TaskManagementSystem.Application.Task.Commands.CreateTask;
using TaskManagementSystem.Application.Task.Commands.UpdateTask;
using TaskManagementSystem.Infrastructure.Context;
using TaskManagementSystem.Infrastructure.Helpers;
using TaskManagementSystem.Infrastructure.Models.Entities;
using TaskManagementSystem.Infrastructure.Models.Options;

namespace TaskManagementSystem.Infrastructure.Services
{
    public class TaskService : ITaskService
    {

        private readonly AppDBContext _db;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly DirectoriesPathsOptions _directoriesPathsOptions;
        private readonly ILogger<TaskService> _logger;

        public TaskService(AppDBContext db,
                           IMapper mapper,
                           IWebHostEnvironment webHostEnvironment,
                           IOptions<DirectoriesPathsOptions> directoriesPathsOptions,
                           ILogger<TaskService> logger)
        {
            _db = db;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
            _directoriesPathsOptions = directoriesPathsOptions?.Value ?? throw new ArgumentNullException(nameof(directoriesPathsOptions));
            _logger = logger;
        }

        public async Task<CommandResponse<TaskDto>> CreateAsync(CreateTaskCommandModel model)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == model.AssignedTo);

            if(user == null)
                return new CommandResponse<TaskDto>(StatusCode.NotFound, "Assigned user not found");

            try
            {
                var task = _mapper.Map<Models.Entities.Task>(model);

                if (model.AttachedFiles != null && model.AttachedFiles.Any())
                {
                    string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, _directoriesPathsOptions.TasksFilesPath);

                    var fileNames = await ServiceHelper.UploadFilesAsync(directoryPath, model.AttachedFiles);

                    task.AttachmentFileNames = string.Join(";", fileNames);
                }

                task.Id = Guid.NewGuid().ToString();
                var newTask = await _db.Tasks.AddAsync(task);
                await _db.SaveChangesAsync();

                var mappedTask = _mapper.Map<TaskDto>(newTask.Entity);

                return new CommandResponse<TaskDto>(StatusCode.Success, null, mappedTask);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CommandResponse<TaskDto>(StatusCode.GenericError, ex.Message);
            }
        }

        public async Task<CommandResponse<string>> DeleteAsync(string id)
        {
            var task = await _db.Tasks.FindAsync(id);

            if (task == null)
                return new CommandResponse<string>(StatusCode.NotFound, "Task could not be found.", id);

            try
            {
                _db.Tasks.Remove(task);
                await _db.SaveChangesAsync();

                return new CommandResponse<string>(StatusCode.Success, "Deleted");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CommandResponse<string>(StatusCode.GenericError, ex.Message);
            }
        }

        public async Task<CommandResponse<TaskDto>> GetByIdAsync(string id)
        {
            var task = await _db.Tasks.FindAsync(id);

            if (task == null)
                return new CommandResponse<TaskDto>(StatusCode.NotFound, "Task could not be found.");

            var taskDto = _mapper.Map<TaskDto>(task);

            return new CommandResponse<TaskDto>(StatusCode.Success, null, taskDto);
        }

        public async Task<CommandResponse<IEnumerable<TaskDto>>> GetAllAsync()
        {
            var tasks = await _db.Tasks.ToListAsync();

            var mappedTasks = _mapper.Map<IEnumerable<TaskDto>>(tasks);

            return new CommandResponse<IEnumerable<TaskDto>>(StatusCode.Success, null, mappedTasks);
        }

        public async Task<CommandResponse<TaskDto>> UpdateAsync(string id, UpdateTaskCommandModel model)
        {
            try
            {
                var user = await _db.Users.SingleOrDefaultAsync(x => x.UserName == model.AssignedTo);

                if (user == null)
                    return new CommandResponse<TaskDto>(StatusCode.NotFound, "Assigned user not found");

                var task = _mapper.Map<Models.Entities.Task>(model);

                if (model.AttachedFiles != null && model.AttachedFiles.Any())
                {
                    string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, _directoriesPathsOptions.TasksFilesPath);

                    var fileNames = await ServiceHelper.UploadFilesAsync(directoryPath, model.AttachedFiles);

                    task.AttachmentFileNames = string.Join(";", fileNames);
                }

                task.Id = id;
                var updatedTask = _db.Tasks.Update(task);
                await _db.SaveChangesAsync();

                var mappedTask = _mapper.Map<TaskDto>(updatedTask.Entity);

                return new CommandResponse<TaskDto>(StatusCode.Success, null, mappedTask);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return new CommandResponse<TaskDto>(StatusCode.GenericError, ex.Message);
            }
        }
    }
}
