namespace FactoryMonitoring.Console;

public interface IAlarmSource
{
    bool HasAlarm();

    string GetAlarmMessage();
}