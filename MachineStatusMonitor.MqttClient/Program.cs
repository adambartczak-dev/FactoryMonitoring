using MQTTnet;
using MQTTnet.Protocol;

var mqttFactory = new MqttClientFactory();

using var mqttClient = mqttFactory.CreateMqttClient();

var options = new MqttClientOptionsBuilder()
    .WithTcpServer("localhost", 1883)
    .Build();

mqttClient.ApplicationMessageReceivedAsync += e =>
{
    string topic = e.ApplicationMessage.Topic;
    string message = e.ApplicationMessage.ConvertPayloadToString();

    Console.WriteLine();
    Console.WriteLine($"Odebrano: {topic} = {message}");

    return Task.CompletedTask;
};

mqttClient.DisconnectedAsync += async e =>
{
    Console.WriteLine();
    Console.WriteLine("Utracono połączenie z brokerem.");

    while (!mqttClient.IsConnected)
    {
        try
        {
            Console.WriteLine("Ponowna próba za 3 sekundy...");

            await Task.Delay(3000);

            await mqttClient.ConnectAsync(options);

            Console.WriteLine("Ponownie połączono z brokerem.");
        }
        catch
        {
            Console.WriteLine("Broker nadal niedostępny.");
        }
    }
};

await mqttClient.ConnectAsync(options);

Console.WriteLine("Połączono z brokerem MQTT.");

var subscribeOptions = mqttFactory
    .CreateSubscribeOptionsBuilder()
    .WithTopicFilter(
        "factory/machine1/temperature",
        MqttQualityOfServiceLevel.AtLeastOnce)
    .Build();

await mqttClient.SubscribeAsync(subscribeOptions);

Console.WriteLine("Subskrypcja aktywna.");
Console.WriteLine();

while (true)
{
    Console.Write("Podaj temperaturę lub q, aby zakończyć: ");

    string? input = Console.ReadLine();

    if (input == "q")
    {
        break;
    }

    if (!mqttClient.IsConnected)
    {
        Console.WriteLine("Brak połączenia z brokerem.");
        continue;
    }

    var message = new MqttApplicationMessageBuilder()
        .WithTopic("factory/machine1/temperature")
        .WithPayload(input ?? "")
        .WithQualityOfServiceLevel(MqttQualityOfServiceLevel.AtLeastOnce)
        .WithRetainFlag()
        .Build();

    await mqttClient.PublishAsync(message);
}

if (mqttClient.IsConnected)
{
    await mqttClient.DisconnectAsync();
}

Console.WriteLine("Rozłączono z brokerem.");