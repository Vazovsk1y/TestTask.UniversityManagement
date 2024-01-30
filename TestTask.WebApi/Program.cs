using TestTask.DAL.Extensions;
using TestTask.Application.Extensions;
using TestTask.WebApi.Extensions;
using TestTask.WebApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddDataAccessLayer(builder.Configuration)
    .AddApplicationLayer();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapStudentEndpoints();
app.MapSpecialityEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MigrateDatabase();
    app.SeedDatabase();
}

app.Run();

