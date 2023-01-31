using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.User.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandValidator : AbstractValidator<AuthenticateUserCommand>
    {
        public AuthenticateUserCommandValidator()
        {
            _ = RuleFor(x => x.Model.Username)
                .NotEmpty();

            _ = RuleFor(x => x.Model.Password)
                .NotEmpty();
        }
    }
}
