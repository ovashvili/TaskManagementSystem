using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;

namespace TaskManagementSystem.Application.User.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<CommandResponse<AuthenticateUserResponse>>
    {
        public AuthenticateUserCommandModel Model { get; set; }
    }

    public class AuthenticateUserResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string Lastname { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }

    public class AuthenticateUserCommandModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }

}
