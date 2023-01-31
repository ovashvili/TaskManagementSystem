using FluentValidation;

namespace TaskManagementSystem.Application.User.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator()
        {
            _ = RuleFor(x => x.Id)
                .NotEmpty();
        }
    }
}
