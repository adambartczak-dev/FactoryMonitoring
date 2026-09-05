namespace FactoryMonitoring.Console;

public class ProductionMachine : Machine
{
    public string ProductName { get; }

    public int UnitsProduced { get; private set; }

    public ProductionMachine(
        string name,
        int temperature,
        MachineStatus status,
        string productName)
        : base(name, temperature, status)
    {
        if (string.IsNullOrWhiteSpace(productName))
        {
            throw new ArgumentException(
                "Product name cannot be empty.",
                nameof(productName)
            );
        }

        ProductName = productName;
        UnitsProduced = 0;
    }

    public void Produce(int amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(amount),
                "Produced amount must be greater than zero."
            );
        }

        UnitsProduced += amount;
    }
}