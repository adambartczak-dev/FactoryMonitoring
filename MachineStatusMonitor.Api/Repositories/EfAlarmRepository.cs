using Microsoft.EntityFrameworkCore;
using MachineStatusMonitor.Api.Data;
using MachineStatusMonitor.Api.Models;

namespace MachineStatusMonitor.Api.Repositories;

public class EfAlarmRepository : IAlarmRepository
{
    private readonly AppDbContext _context;

    public EfAlarmRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Alarm> GetByMachineId(int machineId)
    {
        return _context.Alarms
            .Include(a => a.Machine)
            .Where(a => a.MachineId == machineId)
            .OrderByDescending(a => a.CreatedAt)
            .ToList();
    }

    public Alarm? GetById(int id)
    {
        return _context.Alarms
            .Include(a => a.Machine)
            .FirstOrDefault(a => a.Id == id);
    }

    public Alarm Add(Alarm alarm)
    {
        _context.Alarms.Add(alarm);
        _context.SaveChanges();

        return alarm;
    }
}