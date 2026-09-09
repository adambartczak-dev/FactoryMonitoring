using MachineStatusMonitor.Api.Models;

namespace MachineStatusMonitor.Api.Services;

public interface IMachineService
{
    List<Machine> GetAll();

    Machine? GetById(int id);

    List<Machine> GetByTemperatureRange(
        int minTemperature,
        int maxTemperature);

    Machine Create(Machine machine);

    bool Update(int id, Machine updatedMachine);

    bool Delete(int id);
}