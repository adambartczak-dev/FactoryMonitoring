using System.ComponentModel.DataAnnotations;

namespace MachineStatusMonitor.Api.DTOs;

public class CreateMachineDto
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    [Range(-50, 200)]
    public int Temperature { get; set; }
}