using Customer.Application.Command;
using FluentValidation;

namespace Customer.Application.validator;

public class UpdateAddressCommandValidator :AbstractValidator<UpdateAddressCommand>
{
    public UpdateAddressCommandValidator()
    {
        RuleFor(x => x.Street).NotNull().NotEmpty().WithMessage("Street is required")
            .MaximumLength(70).WithMessage("Street must not exceed 100 characters");
        RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("City is required")
            .MaximumLength(50).WithMessage("City must not exceed 100 characters");
        RuleFor(x => x.State).NotNull().NotEmpty().WithMessage("State is required")
            .MaximumLength(100).WithMessage("State must not exceed 100 characters");
        RuleFor(x => x.ZipCode).NotNull().NotEmpty().WithMessage("ZipCode is required")
            .MaximumLength(10).WithMessage("ZipCode must not exceed 100 characters");
    }
}