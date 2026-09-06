namespace MachineStatusMonitor;

public class Alarm
{
    public string Message { get; set; }
    public DateTime Timestamp { get; set; }

    public Alarm(string message)
    {
        Message = message;
        Timestamp = DateTime.Now;
    }
}