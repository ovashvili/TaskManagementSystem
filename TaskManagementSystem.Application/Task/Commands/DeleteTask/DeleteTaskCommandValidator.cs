using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.User.Commands.DeleteUser;

namespace TaskManagementSystem.Application.Task.Commands.DeleteTask
{
    public class DeleteTaskCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteTaskCommandValidator()
        {
            _ = RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
