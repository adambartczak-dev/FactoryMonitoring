using MachineStatusMonitor;
using System.Text.Json;

AppConfig config;

try
{
    string json = File.ReadAllText("config.json");

    config = JsonSerializer.Deserialize<AppConfig>(json);
}
catch (FileNotFoundException)
{
    Console.WriteLine(
        "ERROR: config.json was not found."
    );

    return;
}
catch (JsonException)
{
    Console.WriteLine(
        "ERROR: config.json contains invalid JSON."
    );

    return;
}
catch (Exception ex)
{
    Console.WriteLine($"ERROR: {ex.Message}");

    return;
}

List<Machine> machines = new List<Machine>();

for (int i = 1; i <= config.MachineCount; i++)
{
    machines.Add(new Machine($"Machine {i}"));
}

string alarmFile = "alarms.csv";

try
{
    if (!File.Exists(alarmFile))
    {
        File.WriteAllText(
            alarmFile,
            "Date,Machine,Temperature,Status\n"
        );
    }
}
catch (Exception ex)
{
    Console.WriteLine(
        $"ERROR creating alarm file: {ex.Message}"
    );
}

int alarmCount = 0;

while (true)
{
    Console.Clear();

    Console.WriteLine(
        "=== FACTORY MONITORING SYSTEM ==="
    );

    Console.WriteLine(
        $"Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
    );

    Console.WriteLine();

    foreach (Machine machine in machines)
    {
        machine.Temperature =
            Random.Shared.Next(20, 101);

        MachineStatus status =
            GetMachineStatus(
                machine.Temperature,
                config
            );

        Console.WriteLine(
            $"{machine.Name,-12} | " +
            $"{machine.Temperature,3}°C | " +
            $"{status}"
        );

        if (status == MachineStatus.Alarm)
        {
            SaveAlarm(alarmFile, machine);

            alarmCount++;
        }
    }

    Console.WriteLine();
    Console.WriteLine(
        $"Alarms this session: {alarmCount}"
    );

    Console.WriteLine(
        $"Refresh interval: {config.RefreshInterval} ms"
    );

    Thread.Sleep(config.RefreshInterval);
}

static MachineStatus GetMachineStatus(
    int temperature,
    AppConfig config)
{
    if (temperature >= config.AlarmTemperature)
    {
        return MachineStatus.Alarm;
    }

    if (temperature >= config.WarningTemperature)
    {
        return MachineStatus.Warning;
    }

    return MachineStatus.Ok;
}

static void SaveAlarm(
    string fileName,
    Machine machine)
{
    string csvLine =
        $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}," +
        $"{machine.Name}," +
        $"{machine.Temperature}," +
        $"ALARM";

    try
    {
        File.AppendAllText(
            fileName,
            csvLine + "\n"
        );
    }
    catch (Exception ex)
    {
        Console.WriteLine(
            $"ERROR saving alarm: {ex.Message}"
        );
    }
}