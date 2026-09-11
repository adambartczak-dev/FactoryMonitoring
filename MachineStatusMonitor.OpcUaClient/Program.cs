using MachineStatusMonitor.OpcUaClient;
using Opc.Ua;
using Opc.Ua.Client;
using System.Net.Http.Json;
using System.Text.Json.Nodes;

string serverUrl =
    "opc.tcp://desktop-unvg1g1:62541/Quickstarts/ReferenceServer";

string apiBaseUrl =
    "https://localhost:7265";

int machineId = 2;

using var httpClient = new HttpClient();

var mqttPublisher = new MqttPublisher();

await mqttPublisher.ConnectAsync();

Console.WriteLine("Machine Status Monitor - OPC UA Client");
Console.WriteLine($"OPC UA Server: {serverUrl}");
Console.WriteLine($"REST API: {apiBaseUrl}");

var config = new ApplicationConfiguration
{
    ApplicationName = "MachineStatusMonitor.OpcUaClient",
    ApplicationType = ApplicationType.Client,

    SecurityConfiguration = new SecurityConfiguration
    {
        ApplicationCertificate = new CertificateIdentifier
        {
            StoreType = "Directory",
            StorePath =
                "%LocalApplicationData%/OPC Foundation/pki/own",
            SubjectName =
                "CN=MachineStatusMonitor.OpcUaClient"
        },

        TrustedIssuerCertificates = new CertificateTrustList
        {
            StoreType = "Directory",
            StorePath =
                "%LocalApplicationData%/OPC Foundation/pki/issuer"
        },

        TrustedPeerCertificates = new CertificateTrustList
        {
            StoreType = "Directory",
            StorePath =
                "%LocalApplicationData%/OPC Foundation/pki/trusted"
        },

        RejectedCertificateStore = new CertificateTrustList
        {
            StoreType = "Directory",
            StorePath =
                "%LocalApplicationData%/OPC Foundation/pki/rejected"
        },

        AutoAcceptUntrustedCertificates = true
    },

    TransportQuotas = new TransportQuotas
    {
        OperationTimeout = 15000
    },

    ClientConfiguration = new ClientConfiguration
    {
        DefaultSessionTimeout = 60000
    }
};

await config.Validate(ApplicationType.Client);

EndpointDescription endpointDescription =
    CoreClientUtils.SelectEndpoint(
        config,
        serverUrl,
        useSecurity: false);

var endpointConfiguration =
    EndpointConfiguration.Create(config);

var endpoint = new ConfiguredEndpoint(
    null,
    endpointDescription,
    endpointConfiguration);

using Session session = await Session.Create(
    config,
    endpoint,
    false,
    "MachineStatusMonitorSession",
    60000,
    new UserIdentity(),
    null);

Console.WriteLine();
Console.WriteLine("Połączono z OPC UA Serverem!");

NodeId nodeId =
    NodeId.Parse("ns=2;s=Scalar_Static_Int32");

var subscription =
    new Subscription(session.DefaultSubscription)
    {
        PublishingInterval = 1000
    };

var monitoredItem =
    new MonitoredItem(subscription.DefaultItem)
    {
        DisplayName = "Temperature",
        StartNodeId = nodeId,
        AttributeId = Attributes.Value,
        SamplingInterval = 500,
        QueueSize = 10,
        DiscardOldest = true
    };

monitoredItem.Notification += OnNotification;

subscription.AddItem(monitoredItem);

session.AddSubscription(subscription);

subscription.Create();

Console.WriteLine("Subscription utworzona.");
Console.WriteLine($"Obserwowana maszyna: {machineId}");
Console.WriteLine(
    "Obserwowany node: ns=2;s=Scalar_Static_Int32");

Console.WriteLine();

while (true)
{
    Console.Write(
        "Podaj nową temperaturę lub wpisz q, aby zakończyć: ");

    string? input = Console.ReadLine();

    if (input?.ToLower() == "q")
    {
        break;
    }

    if (!int.TryParse(input, out int newTemperature))
    {
        Console.WriteLine("Niepoprawna liczba.");
        continue;
    }

    var valueToWrite = new WriteValue
    {
        NodeId = nodeId,
        AttributeId = Attributes.Value,
        Value =
            new DataValue(
                new Variant(newTemperature))
    };

    var valuesToWrite =
        new WriteValueCollection
        {
            valueToWrite
        };

    session.Write(
        null,
        valuesToWrite,
        out StatusCodeCollection results,
        out DiagnosticInfoCollection diagnosticInfos);

    Console.WriteLine(
        $"OPC UA Write: {results[0]}");
}

subscription.Delete(true);

session.Close();

Console.WriteLine("Rozłączono z OPC UA.");

await mqttPublisher.DisconnectAsync();

Console.WriteLine("Rozłączono z MQTT.");

async void OnNotification(
    MonitoredItem item,
    MonitoredItemNotificationEventArgs e)
{
    foreach (var value in item.DequeueValues())
    {
        if (value.Value is not int temperature)
        {
            Console.WriteLine(
                "Otrzymana wartość nie jest typu Int32.");

            continue;
        }

        Console.WriteLine();
        Console.WriteLine(
            $"[OPC UA] Nowa temperatura: {temperature}");

        try
        {
            await UpdateMachineTemperatureAsync(temperature);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"[API] Błąd: {ex.Message}");
        }

        try
        {
            await mqttPublisher.PublishTemperatureAsync(
                temperature);
        }
        catch (Exception ex)
        {
            Console.WriteLine(
                $"[MQTT] Błąd: {ex.Message}");
        }

        Console.Write(
            "Podaj nową temperaturę lub wpisz q, aby zakończyć: ");
    }
}

async Task UpdateMachineTemperatureAsync(int temperature)
{
    string machineUrl =
        $"{apiBaseUrl}/api/machines/{machineId}";

    JsonObject? machine =
        await httpClient.GetFromJsonAsync<JsonObject>(
            machineUrl);

    if (machine is null)
    {
        Console.WriteLine(
            "[API] Nie udało się pobrać maszyny.");

        return;
    }

    if (machine.ContainsKey("temperature"))
    {
        machine["temperature"] = temperature;
    }
    else if (machine.ContainsKey("Temperature"))
    {
        machine["Temperature"] = temperature;
    }
    else
    {
        Console.WriteLine(
            "[API] Maszyna nie posiada pola temperature.");

        return;
    }

    HttpResponseMessage response =
        await httpClient.PutAsJsonAsync(
            machineUrl,
            machine);

    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine(
            $"[API] Temperatura {temperature} zapisana dla maszyny {machineId}.");
    }
    else
    {
        string error =
            await response.Content.ReadAsStringAsync();

        Console.WriteLine(
            $"[API] Błąd HTTP {(int)response.StatusCode}");

        Console.WriteLine(error);
    }
}