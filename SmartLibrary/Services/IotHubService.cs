using Microsoft.Azure.Devices.Client;
using SmartLibrary.Models;
using System.Diagnostics;
using System.Text;

namespace SmartLibrary.Services;

public class IotHubService
{
    private readonly DeviceClient _deviceClient;

    public IotHubService(DeviceConfigurationModel deviceConfig)
    {
        _deviceClient = DeviceClient.CreateFromConnectionString(deviceConfig.ConnectionString);
    }
    public async Task SendMessageAsync(string messageString)
    {
        try
        {
            var message = new Message(Encoding.UTF8.GetBytes(messageString));
            await _deviceClient.SendEventAsync(message);
        }
        catch(Exception ex) 
        {
            Debug.WriteLine(ex.Message);
        }
    }

    public async Task ReceiveMessageAsync()
    {
        while (true)
        {
            try
            {
                var receivedMessage = await _deviceClient.ReceiveAsync();
                if (receivedMessage != null)
                {

                    string messageData = Encoding.UTF8.GetString(receivedMessage.GetBytes());
                    Console.WriteLine("Received message: {0}", messageData);
                    await _deviceClient.CompleteAsync(receivedMessage);
                }
            }
            catch( Exception ex )
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
