using MachineStatusMonitor.Api.Models;

namespace MachineStatusMonitor.Api.Repositories;

public interface IAlarmRepository
{
    List<Alarm> GetByMachineId(int machineId);

    Alarm? GetById(int id);

    Alarm Add(Alarm alarm);
}