using System.ComponentModel.DataAnnotations;

namespace MachineStatusMonitor.Api.DTOs;

public class CreateAlarmDto
{
    [Required]
    public string Message { get; set; } = string.Empty;
}