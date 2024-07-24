using FluentValidation;
using TestTask.Application.Contracts;
using TestTask.Application.Services.Interfaces;
using TestTask.Application.Shared;
using TestTask.DAL.PostgreSQL.Constants;
using TestTask.DAL.PostgreSQL.Extensions;
using TestTask.DAL.PostgreSQL.Interfaces;
using TestTask.DAL.PostgreSQL.Models;

namespace TestTask.Application.Services;

internal class EducationContractService(
    IDbConnectionFactory connectionFactory,
    IValidator<EducationContractRenewalDTO> educationContractRenewalDtoValidator,
    ICalendar calendar) : IEducationContractService
{
    private readonly IDbConnectionFactory _connectionFactory = connectionFactory;
    private readonly IValidator<EducationContractRenewalDTO> _educationContractRenewalDtoValidator = educationContractRenewalDtoValidator;
    private readonly ICalendar _calendar = calendar;

    public async Task<Result> RenewalAsync(EducationContractRenewalDTO renewalDTO, CancellationToken cancellationToken = default)
    {
        var validationResult = _educationContractRenewalDtoValidator.Validate(renewalDTO);
        if (!validationResult.IsValid)
        {
            return Result.Failure(validationResult.ToString());
        }

        using var connection = _connectionFactory.Create();
        connection.Open();

        var previousContract = await connection.GetByOrDefaultAsync<EducationContract, Guid>(Tables.EducationContracts, nameof(EducationContract.student_id), renewalDTO.StudentId);
        if (previousContract is null)
        {
            return Result.Failure(Errors.EntityNotFound(nameof(EducationContract)));
        }

        if (!await connection.IsExistsByAsync(Tables.Specialities, nameof(Speciality.id), renewalDTO.SpecialityId))
        {
            return Result.Failure(Errors.EntityNotFound(nameof(Speciality)));
        }

        var newContract = new EducationContract
        {
            speciality_id = renewalDTO.SpecialityId,
            student_id = previousContract.student_id,
            conclusion_date = _calendar.Today(),
            admission_date = renewalDTO.AdmissionDate,
            graduation_date = renewalDTO.GraduationDate,
            education_form = renewalDTO.EducationForm.Trim(),
        };

        using var transaction = connection.BeginTransaction();
        try
        {
            await transaction.UpdateByAsync(Tables.EducationContracts, nameof(EducationContract.student_id), newContract);
            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }

        return Result.Success();
    }
}