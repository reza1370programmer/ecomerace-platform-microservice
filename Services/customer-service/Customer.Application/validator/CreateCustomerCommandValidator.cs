using Customer.Application.Command;
using FluentValidation;

namespace Customer.Application.validator;

public class CreateCustomerCommandValidator:AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("First name is required")
            .MaximumLength(100).WithMessage("First name cannot exceed 100 characters");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("Last name is required")
            .MaximumLength(100).WithMessage("Last name cannot exceed 100 characters");
        RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is required");
        RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().WithMessage("Phone number is required");
    }
}