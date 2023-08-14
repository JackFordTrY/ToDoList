using FluentValidation;
using ToDoList.Contracts.UserTask;

namespace ToDoList.WebUI.Validators;

public class UserTaskUpdateRequestValidator : AbstractValidator<UserTaskUpdateRequest>
{
	public UserTaskUpdateRequestValidator()
	{
		When(r => r.Title is not null, () =>
			RuleFor(r => r.Title)
				.MinimumLength(5)
				.MaximumLength(100)
		);
    }
}
