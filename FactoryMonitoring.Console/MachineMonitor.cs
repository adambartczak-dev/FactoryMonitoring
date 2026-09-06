namespace MachineStatusMonitor;

public class MachineMonitor
{
    private readonly List<Machine> machines;

    public MachineMonitor(List<Machine> machines)
    {
        this.machines = machines;
    }

    public void ShowAllMachines()
    {
        foreach (Machine machine in machines)
        {
            machine.ShowMachineInfo();
            Console.WriteLine();
        }
    }

    public void ShowHotMachines(int minimumTemperature)
    {
        var hotMachines = machines
            .Where(machine =>
                machine.Temperature > minimumTemperature)
            .OrderByDescending(machine =>
                machine.Temperature);

        Console.WriteLine(
            $"MACHINES ABOVE {minimumTemperature}°C:"
        );

        foreach (Machine machine in hotMachines)
        {
            Console.WriteLine(
                $"{machine.Name}: {machine.Temperature}°C"
            );
        }
    }

    public void ShowMachinesByStatus(MachineStatus status)
    {
        var filteredMachines = machines
            .Where(machine =>
                machine.Status == status)
            .OrderBy(machine =>
                machine.Name);

        Console.WriteLine($"STATUS: {status}");

        foreach (Machine machine in filteredMachines)
        {
            Console.WriteLine(machine.Name);
        }
    }

    public void ShowSummary()
    {
        Console.WriteLine("=== SUMMARY ===");

        Console.WriteLine(
            $"All machines: {machines.Count}"
        );

        Console.WriteLine(
            $"Running: {machines.Count(machine =>
                machine.Status == MachineStatus.Running)}"
        );

        Console.WriteLine(
            $"Warning: {machines.Count(machine =>
                machine.Status == MachineStatus.Warning)}"
        );

        Console.WriteLine(
            $"Stopped: {machines.Count(machine =>
                machine.Status == MachineStatus.Stopped)}"
        );

        bool hasHotMachine =
            machines.Any(machine =>
                machine.Temperature > 80);

        Console.WriteLine(
            $"Machine above 80°C: {hasHotMachine}"
        );
    }

    public void CheckAllMachines()
    {
        foreach (Machine machine in machines)
        {
            machine.CheckTemperature();
        }
    }

    public void ShowAllAlarms()
    {
        foreach (Machine machine in machines)
        {
            Console.WriteLine(
                $"ALARMS - {machine.Name}:"
            );

            machine.AlarmManager.ShowAlarms();

            Console.WriteLine();
        }
    }
}