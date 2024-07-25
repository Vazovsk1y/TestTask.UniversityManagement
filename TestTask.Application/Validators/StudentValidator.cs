using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.DAL.PostgreSQL.Constants;

namespace TestTask.Application.Validators;

public class StudentUpdateDTOValidator : AbstractValidator<StudentUpdateDTO>
{
    public StudentUpdateDTOValidator()
    {
        RuleFor(e => e.Id)
            .NotEmpty();

        RuleFor(e => e.FirstName)
            .NotEmpty()
            .MaximumLength(Constraints.Student.MaxNameLength);

        RuleFor(e => e.LastName)
            .NotEmpty()
            .MaximumLength(Constraints.Student.MaxNameLength);
    }
}
