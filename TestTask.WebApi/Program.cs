using TestTask.DAL.Extensions;
using TestTask.Application.Extensions;
using TestTask.WebApi.Extensions;
using TestTask.WebApi.Endpoints;
using TestTask.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddExceptionHandler<ExceptionsHandler>();
builder.Services.AddProblemDetails();

builder.Services
    .AddDataAccessLayer(builder.Configuration)
    .AddApplicationLayer();

var app = builder.Build();

app.UseExceptionHandler();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapStudentEndpoints();
app.MapSpecialityEndpoints();
app.MapEducationContractEndpoints();
app.MapDepartamentEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MigrateDatabase();
    app.SeedDatabase();
}

app.Run();

