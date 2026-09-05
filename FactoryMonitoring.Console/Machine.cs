namespace MachineStatusMonitor;

public class Machine
{
    public string Name { get; }

    public int Temperature { get; private set; }

    public MachineStatus Status { get; private set; }

    public Machine(string name, int temperature, MachineStatus status)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Machine name cannot be empty.",
                nameof(name)
            );
        }

        Name = name;
        UpdateTemperature(temperature);
        Status = status;
    }

    public void UpdateTemperature(int temperature)
    {
        if (temperature < -50 || temperature > 200)
        {
            throw new ArgumentOutOfRangeException(
                nameof(temperature),
                "Temperature must be between -50 and 200."
            );
        }

        Temperature = temperature;
    }

    public void ChangeStatus(MachineStatus status)
    {
        Status = status;
    }
}