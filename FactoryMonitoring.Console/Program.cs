using MachineStatusMonitor;

Machine machine = new Machine(
    "Machine 1",
    50,
    MachineStatus.Running
);

Console.WriteLine(machine.Name);
Console.WriteLine(machine.Temperature);
Console.WriteLine(machine.Status);

machine.UpdateTemperature(75);
machine.ChangeStatus(MachineStatus.Stopped);

Console.WriteLine();
Console.WriteLine(machine.Temperature);
Console.WriteLine(machine.Status);
