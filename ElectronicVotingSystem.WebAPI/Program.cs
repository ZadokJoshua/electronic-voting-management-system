using ElectronicVotingSystem.WebAPI.DbContexts;
using ElectronicVotingSystem.WebAPI.Extensions;
using ElectronicVotingSystem.WebAPI.Interfaces;
using ElectronicVotingSystem.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddDbContext<ElectronicVotingSystemDbContext>(dbContextOptions =>
{
    dbContextOptions.UseSqlServer(builder.Configuration["AzureSqlServer:ConnectionString"]);
});

builder.Services.ConfigureRepositories();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();