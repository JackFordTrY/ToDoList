using FluentValidation;
using ToDoList.Contracts.User;

namespace ToDoList.WebUI.Validators;

public class UserAuthRequestValidator : AbstractValidator<UserAuthRequest>
{
	public UserAuthRequestValidator()
	{
		RuleFor(r => r.Username)
            .NotEmpty()
            .MinimumLength(5)
            .MaximumLength(20);
        RuleFor(r => r.Password)
            .NotEmpty()
            .MinimumLength(8);
    }
}
