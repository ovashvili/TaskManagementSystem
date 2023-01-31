using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.Common.Models;
using TaskManagementSystem.Application.Contracts;

namespace TaskManagementSystem.Application.User.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, CommandResponse<AuthenticateUserResponse>>
    {
        private readonly IUserService _userService;

        public AuthenticateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public Task<CommandResponse<AuthenticateUserResponse>> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            return _userService.AuthenticateAsync(request.Model);
        }
    }
}
