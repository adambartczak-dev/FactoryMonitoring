using MachineStatusMonitor.Api.Models;

namespace MachineStatusMonitor.Api.Repositories;

public interface IMachineRepository
{
    List<Machine> GetAll();

    Machine? GetById(int id);

    Machine Add(Machine machine);

    bool Update(int id, Machine updatedMachine);

    bool Remove(int id);
}