namespace MachineStatusMonitor;

class Machine
{
    public string Name { get; set; }
    public int Temperature { get; set; }
    public bool IsRunning { get; set; }

    public Machine()
    {
    }

    public Machine(string name)
    {
        Name = name;
        Temperature = 20;
        IsRunning = true;
    }
}