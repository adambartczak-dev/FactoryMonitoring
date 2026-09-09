using MachineStatusMonitor.Api.Models;

namespace MachineStatusMonitor.Api.Repositories;

public class InMemoryMachineRepository : IMachineRepository
{
    private readonly List<Machine> _machines = new()
    {
        new Machine
        {
            Id = 1,
            Name = "Robot 1",
            Temperature = 25
        },
        new Machine
        {
            Id = 2,
            Name = "Robot 2",
            Temperature = 45
        },
        new Machine
        {
            Id = 3,
            Name = "Prasa 1",
            Temperature = 70
        }
    };

    public List<Machine> GetAll()
    {
        return _machines;
    }

    public Machine? GetById(int id)
    {
        return _machines.FirstOrDefault(m => m.Id == id);
    }

    public Machine Add(Machine machine)
    {
        machine.Id = _machines.Count == 0
            ? 1
            : _machines.Max(m => m.Id) + 1;

        _machines.Add(machine);

        return machine;
    }

    public void Remove(Machine machine)
    {
        _machines.Remove(machine);
    }
}