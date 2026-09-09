using MachineStatusMonitor.Api.Models;

namespace MachineStatusMonitor.Api.Repositories;

public interface IMachineRepository
{
    List<Machine> GetAll();

    Machine? GetById(int id);

    Machine Add(Machine machine);

    void Remove(Machine machine);
}