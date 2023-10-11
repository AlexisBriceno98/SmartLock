namespace SmartLibrary.Device.Models;

public class WeatherResponse
{
    public Main? Main { get; set; }
}

public class Main
{
    public double Temp { get; set;}
}
