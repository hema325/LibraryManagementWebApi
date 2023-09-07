using Application;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Host.AddInfrastructure();

builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddMiddlewares();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMiddlewares();

app.UseHttpsRedirection();

app.UseInfrastructure(app.Services);

app.MapControllers();

await app.Services.InitialiseDbAsync();

app.Run();
