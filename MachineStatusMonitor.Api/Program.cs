using MachineStatusMonitor.Api.Repositories;
using MachineStatusMonitor.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddSingleton<IMachineRepository, InMemoryMachineRepository>();

builder.Services.AddScoped<IMachineService, MachineService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();