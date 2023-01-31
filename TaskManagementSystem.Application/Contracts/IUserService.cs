using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using MediatR;
using System.Linq.Expressions;
using TaskManagementSystem.Application.User.Commands.AuthenticateUser;
using TaskManagementSystem.Application.User.Commands.RegisterUser;
using TaskManagementSystem.Application.User.Commands.UpdateUser;

namespace TaskManagementSystem.Application.Contracts
{
    public interface IUserService
    {
        Task<CommandResponse<AuthenticateUserResponse>> AuthenticateAsync(AuthenticateUserCommandModel model);
        Task<CommandResponse<IEnumerable<UserDto>>> GetAllAsync();
        Task<CommandResponse<UserDto>> GetByIdAsync(string id);
        Task<CommandResponse<UserDto>> RegisterAsync(RegisterUserCommandModel model);
        Task<CommandResponse<UserDto>> UpdateAsync(string id, UpdateUserCommandModel model);
        Task<CommandResponse<string>> DeleteAsync(string id);
        Task<bool> AnyAsync();
    }
}
