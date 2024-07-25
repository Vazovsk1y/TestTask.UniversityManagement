using TestTask.Application.Extensions;
using TestTask.WebApi.Extensions;
using TestTask.WebApi.Endpoints;
using TestTask.WebApi;
using Serilog;
using TestTask.DAL.PostgreSQL.Extensions;
using TestTask.WebApi.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, conf) =>
{
    conf.ReadFrom.Configuration(context.Configuration);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<ExceptionsHandler>();
builder.Services.AddProblemDetails();

builder.Services
    .AddDataAccessLayer(builder.Configuration)
    .AddApplicationLayer();

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapStudentEndpoints();
app.MapSpecialityEndpoints();
app.MapEducationContractEndpoints();
app.MapDepartmentEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MigrateDatabase();
    app.SeedDatabase();
}

app.Run();

