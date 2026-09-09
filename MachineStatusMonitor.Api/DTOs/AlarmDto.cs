namespace MachineStatusMonitor.Api.DTOs;

public class AlarmDto
{
    public int Id { get; set; }

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public int MachineId { get; set; }

    public string MachineName { get; set; } = string.Empty;
}