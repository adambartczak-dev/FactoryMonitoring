namespace MachineStatusMonitor;

public abstract class Machine
{
    public string Name { get; set; }
    public int Temperature { get; set; }
    public MachineStatus Status { get; set; }

    public AlarmManager AlarmManager { get; }

    public Machine(
        string name,
        int temperature,
        MachineStatus status)
    {
        Name = name;
        Temperature = temperature;
        Status = status;

        AlarmManager = new AlarmManager();
    }

    public void CheckTemperature()
    {
        if (Temperature > 80)
        {
            AlarmManager.AddAlarm(
                $"{Name}: Temperature too high ({Temperature}°C)"
            );
        }
    }

    public abstract void ShowMachineInfo();
}