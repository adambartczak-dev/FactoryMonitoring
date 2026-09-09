using MachineStatusMonitor.Api.Models;
using MachineStatusMonitor.Api.Repositories;

namespace MachineStatusMonitor.Api.Services;

public class MachineService : IMachineService
{
    private readonly IMachineRepository _machineRepository;

    public MachineService(IMachineRepository machineRepository)
    {
        _machineRepository = machineRepository;
    }

    public List<Machine> GetAll()
    {
        return _machineRepository.GetAll();
    }

    public Machine? GetById(int id)
    {
        return _machineRepository.GetById(id);
    }

    public List<Machine> GetByTemperatureRange(
    int minTemperature,
    int maxTemperature)
    {
        return _machineRepository
            .GetAll()
            .Where(m =>
                m.Temperature >= minTemperature &&
                m.Temperature <= maxTemperature)
            .ToList();
    }

    public Machine Create(Machine machine)
    {
        return _machineRepository.Add(machine);
    }

    public bool Update(int id, Machine updatedMachine)
    {
        return _machineRepository.Update(id, updatedMachine);
    }

    public bool Delete(int id)
    {
        return _machineRepository.Remove(id);
    }
}