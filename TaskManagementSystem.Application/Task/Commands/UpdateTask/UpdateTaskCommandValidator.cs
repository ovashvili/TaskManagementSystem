using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagementSystem.Application.Task.Commands.UpdateTask
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            _ = RuleFor(x => x.Id)
                .NotEmpty();

            _ = RuleFor(x => x.Model.Title)
                .MaximumLength(40)
                .When(s => !string.IsNullOrEmpty(s.Model.Title));

            _ = RuleFor(x => x.Model.AssignedTo)
                .NotEmpty();

            _ = RuleFor(x => x.Model.Description)
                .MaximumLength(60)
                .When(s => !string.IsNullOrEmpty(s.Model.Description));

            _ = RuleFor(x => x.Model.AdditionalDescription)
                .MaximumLength(120)
                .When(s => !string.IsNullOrEmpty(s.Model.Description));
        }
    }
}
