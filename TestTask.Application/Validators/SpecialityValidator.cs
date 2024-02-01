using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.DAL.Constants;

namespace TestTask.Application.Validators;

public class SpecialityAddDTOValidator : AbstractValidator<SpecialityAddDTO>
{
    public SpecialityAddDTOValidator()
    {
        RuleFor(e => e.Title).NotEmpty().MaximumLength(Constraints.Speciality.MaxTitleLength);
        RuleFor(e => e.Code).NotEmpty().MaximumLength(Constraints.Speciality.MaxCodeLength);
    }
}
