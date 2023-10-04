using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using SmartLibrary.Models;
using System.Text;

namespace SmartLibrary.Services;

public class DeviceManager
{
    //Remember
    private readonly DeviceConfigurationModel _config;
    //Listen
    private readonly DeviceClient _client;
    private readonly IotHubService _iotHubService;

    public event Action<string>? OnCommandReceived;
    public DeviceManager(DeviceConfigurationModel config, IotHubService iotHubService)
    {
        _iotHubService = iotHubService;
        _config = config;
        _client = DeviceClient.CreateFromConnectionString(_config.ConnectionString);

        Task.FromResult(StartAsync());
    }

    public bool AllowSending() => _config.AllowSending;

    private async Task StartAsync()
    {
        await _client.SetMethodDefaultHandlerAsync(DirectMethodDefaultCallback, null);
    }


    private async Task<MethodResponse> DirectMethodDefaultCallback(MethodRequest req, object userContext)
    {
        var res = new DirectMethodDataResponse
        {
            Message = $"Executed Method {req.Name} successfully.",
        };

        switch (req.Name.ToLower())
        {
            case "unlock":
                {
                    _config.AllowSending = true;
                    OnCommandReceived?.Invoke("unlock");
                    break;
                }

            case "lock":
                {
                    _config.AllowSending = false;
                    OnCommandReceived?.Invoke("lock");
                    break;
                }

            case "interval":
                {
                    break;
                }

            default:
                {
                    res.Message = $"Direct method {req.Name} not found.";
                    return new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res)), 404);
                }

        }

        return new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res)), 200);
    }

    public async Task SendTelemetryAsync()
    {
        var telemetryData = new
        {
            DeviceId = _config.DeviceId,
            Status = _config.AllowSending ? "Unlocked" : "Locked",
            Timestamp = DateTime.Now,
        };

        var messageString = JsonConvert.SerializeObject(telemetryData);
        await _iotHubService.SendMessageAsync(messageString);
    }
}
