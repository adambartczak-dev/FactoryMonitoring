namespace FactoryMonitoring.Console;

public class TemperatureSensor : IAlarmSource
{
    public string Name { get; }

    public int Temperature { get; private set; }

    public TemperatureSensor(
        string name,
        int temperature)
    {
        Name = name;
        Temperature = temperature;
    }

    public bool HasAlarm()
    {
        return Temperature > 80;
    }

    public string GetAlarmMessage()
    {
        if (HasAlarm())
        {
            return $"ALARM: sensor {Name}, temperature: {Temperature}°C";
        }

        return $"No alarm for sensor {Name}.";
    }
}