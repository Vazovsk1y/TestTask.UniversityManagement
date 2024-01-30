using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TestTask.Application.Services;
using TestTask.Application.Services.Interfaces;

namespace TestTask.Application.Extensions;

public static class Registrator
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<IStudentService, StudentService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<ISpecialityService, SpecialityService>();

        return services;
    }
}