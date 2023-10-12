using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
using SmartLibrary.MVVM.Models;
using System.Diagnostics;
using System.Text;

namespace SmartLibrary.Services;

public class DeviceManager
{
    
    private readonly DeviceConfigurationModel _config;
    
    private readonly DeviceClient _client;
    private readonly IotHubService _iotHubService;
    private readonly ServiceClient _serviceClient;

    public event Action<string>? OnCommandReceived;
    public DeviceManager(IotHubService iotHubService, DeviceConfigurationModel config)
    {
        _iotHubService = iotHubService;
        _config = config;
        _client = DeviceClient.CreateFromConnectionString("HostName=alexis-iothub.azure-devices.net;DeviceId=SmartLock;SharedAccessKey=d7soyFPPoqoVfRzZzrpyvwUaKY3GmU0HuAIoTH+NP0U=");
        _serviceClient = ServiceClient.CreateFromConnectionString("HostName=alexis-iothub.azure-devices.net;DeviceId=SmartLock;SharedAccessKey=d7soyFPPoqoVfRzZzrpyvwUaKY3GmU0HuAIoTH+NP0U=");
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

        await ReportLockStatusAsync();

        return new MethodResponse(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(res)), 200);
    }

    public async Task ReportLockStatusAsync()
    {
        var twinProperties = new TwinCollection();
        twinProperties["LockStatus"] = _config.AllowSending ? "Unlocked" : "Locked";
        await _client.UpdateReportedPropertiesAsync(twinProperties);
    }

    public async Task<string> GetLockStatusAsync()
    {
        var twin = await _client.GetTwinAsync();
        return twin.Properties.Reported["LockStatus"];
    }

}
