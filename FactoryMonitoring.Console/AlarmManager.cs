namespace MachineStatusMonitor;

public class AlarmManager
{
    private List<Alarm> alarms = new();

    public void AddAlarm(string message)
    {
        Alarm alarm = new(message);

        alarms.Add(alarm);
    }

    public void ShowAlarms()
    {
        if (alarms.Count == 0)
        {
            Console.WriteLine("No alarms.");
            return;
        }

        foreach (Alarm alarm in alarms)
        {
            Console.WriteLine(
                $"{alarm.Timestamp}: {alarm.Message}"
            );
        }
    }
}