using Microsoft.AspNetCore.Mvc;
using MachineStatusMonitor.Api.Models;
using MachineStatusMonitor.Api.Services;
using MachineStatusMonitor.Api.DTOs;

namespace MachineStatusMonitor.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MachinesController : ControllerBase
{
    private readonly IMachineService _machineService;

    public MachinesController(IMachineService machineService)
    {
        _machineService = machineService;
    }

    [HttpGet]
    public ActionResult<List<Machine>> GetMachines()
    {
        return Ok(_machineService.GetAll());
    }

    [HttpGet("{id}")]
    public ActionResult<Machine> GetMachine(int id)
    {
        Machine? machine = _machineService.GetById(id);

        if (machine == null)
        {
            return NotFound();
        }

        return Ok(machine);
    }

    [HttpGet("hot")]
    public ActionResult<List<Machine>> GetHotMachines(
    int minTemperature,
    int maxTemperature)
    {
        List<Machine> machines =
            _machineService.GetByTemperatureRange(
                minTemperature,
                maxTemperature);

        return Ok(machines);
    }

    [HttpPost]
    public ActionResult<Machine> CreateMachine(CreateMachineDto dto)
    {
        Machine machine = new Machine
        {
            Name = dto.Name,
            Temperature = dto.Temperature
        };

        Machine createdMachine = _machineService.Create(machine);

        return CreatedAtAction(
            nameof(GetMachine),
            new { id = createdMachine.Id },
            createdMachine
        );
    }

    [HttpPut("{id}")]
    public IActionResult UpdateMachine(
        int id,
        UpdateMachineDto dto)
    {
        Machine updatedMachine = new Machine
        {
            Name = dto.Name,
            Temperature = dto.Temperature
        };

        bool updated = _machineService.Update(id, updatedMachine);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMachine(int id)
    {
        bool deleted = _machineService.Delete(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}