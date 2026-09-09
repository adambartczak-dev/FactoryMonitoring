using Microsoft.AspNetCore.Mvc;
using MachineStatusMonitor.Api.DTOs;
using MachineStatusMonitor.Api.Services;

namespace MachineStatusMonitor.Api.Controllers;

[ApiController]
[Route("api/machines/{machineId:int}/alarms")]
public class AlarmsController : ControllerBase
{
    private readonly AlarmService _service;

    public AlarmsController(AlarmService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll(int machineId)
    {
        var alarms = _service.GetByMachineId(machineId);

        if (alarms is null)
        {
            return NotFound();
        }

        return Ok(alarms);
    }

    [HttpPost]
    public IActionResult Create(
        int machineId,
        CreateAlarmDto dto)
    {
        var alarm = _service.Add(machineId, dto);

        if (alarm is null)
        {
            return NotFound();
        }

        return StatusCode(201, alarm);
    }
}