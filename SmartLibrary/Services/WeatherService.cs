using SmartLibrary.Device.Models;
using System.Text.Json;

namespace SmartLibrary.Services;

public class WeatherService
{
    private const string BaseUrl = "http://api.openweathermap.org/data/2.5/weather?q=";
    private const string ApiKey = "71fa7e3206efffb8ecef537f15421773";

    public async Task <WeatherResponse> GetWeatherAsync(string city)
    {
        using var client = new HttpClient();
        var response = await client.GetStringAsync($"{BaseUrl}{city}&appid={ApiKey}&units=metric");
        return JsonSerializer.Deserialize<WeatherResponse>(response);
    }
}
