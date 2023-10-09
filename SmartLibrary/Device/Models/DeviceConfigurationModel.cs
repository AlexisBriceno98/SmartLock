namespace SmartLibrary.MVVM.Models;

public class DeviceConfigurationModel
{
    private readonly string _connectionString;

    public DeviceConfigurationModel(string connectionString)
    {
        _connectionString = connectionString;
    }

    public string DeviceId => _connectionString.Split(";")[1].Split("=")[1];

    public string ConnectionString => _connectionString;

    public bool AllowSending { get; set; } = false;

    public int TelemetryInterval { get; set; } = 10000;
}
