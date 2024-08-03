using FluentValidation;

namespace TestTaskMegapolis.DTOs.User;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Firstname cannot be empty.");
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Lastname cannot be empty");
    }
}