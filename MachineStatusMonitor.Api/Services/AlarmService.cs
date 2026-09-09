using MachineStatusMonitor.Api.DTOs;
using MachineStatusMonitor.Api.Models;
using MachineStatusMonitor.Api.Repositories;

namespace MachineStatusMonitor.Api.Services;

public class AlarmService
{
    private readonly IAlarmRepository _alarmRepository;
    private readonly IMachineRepository _machineRepository;

    public AlarmService(
        IAlarmRepository alarmRepository,
        IMachineRepository machineRepository)
    {
        _alarmRepository = alarmRepository;
        _machineRepository = machineRepository;
    }

    public List<AlarmDto>? GetByMachineId(int machineId)
    {
        var machine = _machineRepository.GetById(machineId);

        if (machine is null)
        {
            return null;
        }

        var alarms = _alarmRepository.GetByMachineId(machineId);

        return alarms.Select(a => new AlarmDto
        {
            Id = a.Id,
            Message = a.Message,
            CreatedAt = a.CreatedAt,
            MachineId = a.MachineId,
            MachineName = a.Machine.Name
        }).ToList();
    }

    public AlarmDto? Add(int machineId, CreateAlarmDto dto)
    {
        var machine = _machineRepository.GetById(machineId);

        if (machine is null)
        {
            return null;
        }

        var alarm = new Alarm
        {
            Message = dto.Message,
            CreatedAt = DateTime.UtcNow,
            MachineId = machineId
        };

        _alarmRepository.Add(alarm);

        return new AlarmDto
        {
            Id = alarm.Id,
            Message = alarm.Message,
            CreatedAt = alarm.CreatedAt,
            MachineId = alarm.MachineId,
            MachineName = machine.Name
        };
    }
}