namespace MachineStatusMonitor;

class AppConfig
{
    public int WarningTemperature { get; set; }
    public int AlarmTemperature { get; set; }
    public int RefreshInterval { get; set; }
    public int MachineCount { get; set; }
}