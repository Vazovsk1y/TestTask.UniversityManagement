using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.DAL.Constants;

namespace TestTask.Application.Validators;

public class EducationContractRenewalValidator : AbstractValidator<EducationContractRenewalDTO>
{
    public EducationContractRenewalValidator()
    {
        RuleFor(e => e.EducationForm).Must(e => EducationForms.Enumerate().Contains(e));
        RuleFor(e => e.SpecialityId).NotEmpty();
        RuleFor(e => e.StudentId).NotEmpty();
    }
}
