namespace MachineStatusMonitor;

public class RobotMachine : Machine
{
    public int AxisCount { get; set; }

    public RobotMachine(
        string name,
        int temperature,
        MachineStatus status,
        int axisCount)
        : base(name, temperature, status)
    {
        AxisCount = axisCount;
    }

    public override void ShowMachineInfo()
    {
        Console.WriteLine("=== ROBOT MACHINE ===");
        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Temperature: {Temperature}°C");
        Console.WriteLine($"Status: {Status}");
        Console.WriteLine($"Axis count: {AxisCount}");
    }
}