using System.Net.NetworkInformation;

namespace SmartLibrary.Services;

public class NetworkManager
{
    public virtual async Task<string> CheckConnectivityAsync(string ipAdress = "8.8.8.8")
    {
        var isConnected = await SendPingAsync(ipAdress);
        return isConnected ? "Connected" : "Disconnected";
    }

    private async Task<bool> SendPingAsync(string ipAdress)
    {
        try
        {
            using var ping = new Ping();

            var reply = await ping.SendPingAsync(ipAdress, 1000, new byte[32], new());
            return reply.Status == IPStatus.Success;
        }
        catch
        {
            return false;
        }
    }
}
