using MachineStatusMonitor.Api.Data;
using MachineStatusMonitor.Api.Models;

namespace MachineStatusMonitor.Api.Repositories;

public class EfMachineRepository : IMachineRepository
{
    private readonly AppDbContext _context;

    public EfMachineRepository(AppDbContext context)
    {
        _context = context;
    }

    public List<Machine> GetAll()
    {
        return _context.Machines.ToList();
    }

    public Machine? GetById(int id)
    {
        return _context.Machines
            .FirstOrDefault(m => m.Id == id);
    }

    public Machine Add(Machine machine)
    {
        _context.Machines.Add(machine);
        _context.SaveChanges();

        return machine;
    }

    public bool Update(int id, Machine updatedMachine)
    {
        var machine = _context.Machines
            .FirstOrDefault(m => m.Id == id);

        if (machine is null)
        {
            return false;
        }

        machine.Name = updatedMachine.Name;
        machine.Status = updatedMachine.Status;
        machine.Temperature = updatedMachine.Temperature;

        _context.SaveChanges();

        return true;
    }

    public bool Remove(int id)
    {
        var machine = _context.Machines
            .FirstOrDefault(m => m.Id == id);

        if (machine is null)
        {
            return false;
        }

        _context.Machines.Remove(machine);
        _context.SaveChanges();

        return true;
    }
}