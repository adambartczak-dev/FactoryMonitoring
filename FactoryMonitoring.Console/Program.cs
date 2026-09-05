using FactoryMonitoring.Console;
using System.Reflection.PortableExecutable;

ProductionMachine productionMachine = new ProductionMachine(
    "CNC-01",
    55,
    MachineStatus.Running,
    "Gear"
);

Console.WriteLine($"Machine: {productionMachine.Name}");
Console.WriteLine($"Temperature: {productionMachine.Temperature}");
Console.WriteLine($"Status: {productionMachine.Status}");
Console.WriteLine($"Product: {productionMachine.ProductName}");
Console.WriteLine($"Produced: {productionMachine.UnitsProduced}");

productionMachine.Produce(10);
productionMachine.Produce(5);

Console.WriteLine($"Produced after production: {productionMachine.UnitsProduced}");