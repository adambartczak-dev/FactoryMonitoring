namespace MachineStatusMonitor.Api.Models;

public class Machine
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public double Temperature { get; set; }
}