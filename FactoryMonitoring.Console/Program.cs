using FactoryMonitoring.Console;

Machine machine = new Machine(
    "Machine-01",
    90,
    MachineStatus.Running
);

TemperatureSensor sensor = new TemperatureSensor(
    "Sensor-01",
    95
);

Console.WriteLine(machine.GetAlarmMessage());
Console.WriteLine(sensor.GetAlarmMessage());

static void PrintAlarm(IAlarmSource source)
{
    Console.WriteLine(source.GetAlarmMessage());
}

PrintAlarm(machine);
PrintAlarm(sensor);