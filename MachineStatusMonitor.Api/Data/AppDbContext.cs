using Microsoft.EntityFrameworkCore;
using MachineStatusMonitor.Api.Models;

namespace MachineStatusMonitor.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Machine> Machines { get; set; }

    public DbSet<Alarm> Alarms { get; set; }
}