using MachineStatusMonitor;

List<Machine> machines = new()
{
    new ProductionMachine(
        "CNC-01",
        65,
        MachineStatus.Running,
        1200),

    new ProductionMachine(
        "CNC-02",
        92,
        MachineStatus.Warning,
        850),

    new MaintenanceMachine(
        "PRESS-01",
        40,
        MachineStatus.Stopped,
        DateTime.Now.AddDays(-10)),

    new RobotMachine(
        "ROBOT-01",
        85,
        MachineStatus.Running,
        6),

    new RobotMachine(
        "ROBOT-02",
        55,
        MachineStatus.Stopped,
        6)
};

MachineMonitor monitor = new(machines);

monitor.ShowAllMachines();

monitor.ShowSummary();

Console.WriteLine();

monitor.ShowHotMachines(80);

Console.WriteLine();

monitor.ShowMachinesByStatus(
    MachineStatus.Running
);

Console.WriteLine();

monitor.CheckAllMachines();
monitor.ShowAllAlarms();