using FluentValidation;
using ToDoList.Contracts.UserTask;

namespace ToDoList.WebUI.Validators;

public class UserTaskAddRequestValidator : AbstractValidator<UserTaskAddRequest>
{
    public UserTaskAddRequestValidator()
    {
        RuleFor(r => r.Title)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(100);
    }
}
