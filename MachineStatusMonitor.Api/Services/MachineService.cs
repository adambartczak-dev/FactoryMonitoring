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
        Machine? machine = _machineRepository.GetById(id);

        if (machine == null)
        {
            return false;
        }

        machine.Name = updatedMachine.Name;
        machine.Temperature = updatedMachine.Temperature;

        return true;
    }

    public bool Delete(int id)
    {
        Machine? machine = _machineRepository.GetById(id);

        if (machine == null)
        {
            return false;
        }

        _machineRepository.Remove(machine);

        return true;
    }
}