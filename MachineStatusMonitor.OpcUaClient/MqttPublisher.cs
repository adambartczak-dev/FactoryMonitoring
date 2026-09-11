using MQTTnet;
using MQTTnet.Protocol;

namespace MachineStatusMonitor.OpcUaClient;

public class MqttPublisher
{
    private readonly MqttClientFactory _mqttFactory;
    private readonly IMqttClient _mqttClient;
    private readonly MqttClientOptions _options;

    public MqttPublisher()
    {
        _mqttFactory = new MqttClientFactory();

        _mqttClient = _mqttFactory.CreateMqttClient();

        _options = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883)
            .Build();
    }

    public async Task ConnectAsync()
    {
        await _mqttClient.ConnectAsync(_options);

        Console.WriteLine("Połączono z brokerem MQTT.");
    }

    public async Task PublishTemperatureAsync(double temperature)
    {
        var message = new MqttApplicationMessageBuilder()
            .WithTopic("factory/machine1/temperature")
            .WithPayload(temperature.ToString())
            .WithQualityOfServiceLevel(
                MqttQualityOfServiceLevel.AtLeastOnce)
            .WithRetainFlag()
            .Build();

        await _mqttClient.PublishAsync(message);

        Console.WriteLine(
            $"[MQTT] Wysłano temperaturę: {temperature}");
    }

    public async Task DisconnectAsync()
    {
        if (_mqttClient.IsConnected)
        {
            await _mqttClient.DisconnectAsync();
        }
    }
}