namespace MachineStatusMonitor.Api.Models;

public class Alarm
{
    public int Id { get; set; }

    public string Message { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public int MachineId { get; set; }

    public Machine Machine { get; set; } = null!;
}