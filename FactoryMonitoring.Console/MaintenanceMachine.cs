namespace MachineStatusMonitor;

public class MaintenanceMachine : Machine
{
    public DateTime LastMaintenance { get; set; }

    public MaintenanceMachine(
        string name,
        int temperature,
        MachineStatus status,
        DateTime lastMaintenance)
        : base(name, temperature, status)
    {
        LastMaintenance = lastMaintenance;
    }

    public override void ShowMachineInfo()
    {
        Console.WriteLine("=== MAINTENANCE MACHINE ===");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Temperature: {Temperature}°C");
        Console.WriteLine($"Status: {Status}");
        Console.WriteLine($"Last maintenance: {LastMaintenance:d}");
    }
}