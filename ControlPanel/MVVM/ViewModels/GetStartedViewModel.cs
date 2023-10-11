using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlPanel.Views;
using SmartLibrary.Device.Models;
using SmartLibrary.MVVM.Models;
using SmartLibrary.Services;
using System.Collections.ObjectModel;

namespace ControlPanel.MVVM.ViewModels;

public partial class GetStartedViewModel : ObservableObject
{

    private ObservableCollection<DeviceItemModel> _devices;
    private readonly WeatherService _weatherService = new WeatherService();
    public double Temperature { get; private set; }
    public ObservableCollection<DeviceItemModel> Devices
    {
        get => _devices;
        set => SetProperty(ref _devices, value);
    }

    public GetStartedViewModel()
    {
        Devices = new ObservableCollection<DeviceItemModel>
        {
            new DeviceItemModel {DeviceId = "1", DeviceType = "Lock", Vendor = "Alexis", Location = "Entrance", IsActive = true},
        };

        LoadWeatherAsync();
    }

    public async Task LoadWeatherAsync()
    {
        var weather = await _weatherService.GetWeatherAsync("Stockholm");
        Temperature = weather.Main.Temp;
        OnPropertyChanged(nameof(Temperature));
    }

    public string CurrentDate => DateTime.Now.ToString("ddd, MMMM dd, yyyy");
    public string CurrentTime => DateTime.Now.ToString("h:mm tt");

    [RelayCommand]
    async Task GoToSettings()
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }

}
