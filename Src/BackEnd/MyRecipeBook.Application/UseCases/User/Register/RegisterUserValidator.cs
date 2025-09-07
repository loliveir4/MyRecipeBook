using FluentValidation;
using MyRecipeBook.Communication.Requests;
using MyRecipeBook.Exceptions;

namespace MyRecipeBook.Application.UseCases.User.Register;

public class RegisterUserValidator : AbstractValidator<RequestRegisterUserjson>
{
    public RegisterUserValidator()
    {
        RuleFor(User => User.Name).NotEmpty().WithMessage(ResourceMessagesException.NAME_EMPTY);
        RuleFor(User => User.Email).NotEmpty().WithMessage(ResourceMessagesException.EMAIL_EMPTY);
        RuleFor(User => User.Password.Length).GreaterThanOrEqualTo(6).WithMessage(ResourceMessagesException.PASSWORD_INVALID);

        When(When => string.IsNullOrEmpty(When.Email) == false, () =>
        {
            RuleFor(User => User.Email).EmailAddress().WithMessage(ResourceMessagesException.EMAIL_INVALID);
        });

    }
}
