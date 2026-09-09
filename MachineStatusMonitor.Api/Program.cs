using MachineStatusMonitor.Api.Repositories;
using MachineStatusMonitor.Api.Services;
using Microsoft.EntityFrameworkCore;
using MachineStatusMonitor.Api.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IMachineRepository, EfMachineRepository>();
builder.Services.AddScoped<IAlarmRepository, EfAlarmRepository>();

builder.Services.AddScoped<IMachineService, MachineService>();
builder.Services.AddScoped<AlarmService>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();