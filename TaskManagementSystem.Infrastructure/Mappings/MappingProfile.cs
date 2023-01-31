using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.AccessControl;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Task.Commands.CreateTask;
using TaskManagementSystem.Application.Task.Commands.UpdateTask;
using TaskManagementSystem.Application.User.Commands.AuthenticateUser;
using TaskManagementSystem.Application.User.Commands.RegisterUser;
using TaskManagementSystem.Application.User.Commands.UpdateUser;
using TaskManagementSystem.Infrastructure.Models.Entities;

namespace TaskManagementSystem.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserCommandModel, User>();

            CreateMap<UpdateUserCommandModel, User>();

            CreateMap<User, UserDto>();

            CreateMap<User, AuthenticateUserResponse>();

            CreateMap<CreateTaskCommandModel, Models.Entities.Task> ();

            CreateMap<UpdateTaskCommandModel, Models.Entities.Task> ();

            CreateMap<Models.Entities.Task, TaskDto>()
                .ForMember(dest => dest.FileNames,
                    opt => opt.MapFrom((src, dest) => src?.AttachmentFileNames?.Split(';').ToList()));

            CreateMap<IdentityRole, RoleDto>();
        }
    }
}
