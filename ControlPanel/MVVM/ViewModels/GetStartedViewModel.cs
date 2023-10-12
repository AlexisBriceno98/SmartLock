using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlPanel.MVVM.Views;
using ControlPanel.Views;
using Microsoft.Azure.Devices;
using SmartLibrary.Device.Models;
using SmartLibrary.MVVM.Models;
using SmartLibrary.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ControlPanel.MVVM.ViewModels;

public partial class GetStartedViewModel : ObservableObject
{

    private ObservableCollection<DeviceItemModel> _devices;
    private readonly DeviceManager _deviceManager;
    private readonly IotHubService _iotHubService;
    public ObservableCollection<DeviceItemModel> Devices
    {
        get => _devices;
        set => SetProperty(ref _devices, value);
    }

    public GetStartedViewModel(DeviceManager deviceManager, IotHubService iotHubService)
    {
        _deviceManager = deviceManager;
        _iotHubService = iotHubService;
        InitializeAsync();
    }



    private async Task InitializeAsync()
    {
        var lockStatus = await _deviceManager.GetLockStatusAsync();
        IsDeviceConnected = lockStatus == "Locked";
    }

    [RelayCommand]
    async Task GoToSettings()
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    [RelayCommand]
    async Task GoToDeviceList()
    {
        await Shell.Current.GoToAsync(nameof(DeviceListPage));
    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    private string _connectionStatusText;
    public string ConnectionStatusText
    {
        get => _connectionStatusText;
        set => SetProperty(ref _connectionStatusText, value);
    }

    private bool _isConnectionStatusVisible;
    public bool IsConnectionStatusVisible
    {
        get => _isConnectionStatusVisible;
        set => SetProperty(ref _isConnectionStatusVisible, value);
    }

    private bool _isDeviceConnected;
    public bool IsDeviceConnected
    {
        get => _isDeviceConnected;
        set => SetProperty(ref _isDeviceConnected, value);
    }

    public ICommand ToggleStateCommand { get; private set; }

    public async void ToggleState(ToggledEventArgs e)
    {
        bool isToggled = e.Value;
        var deviceId = "SmartLock";
        string commandName = isToggled ? "unlock" : "lock";
        try
        {
            await _iotHubService.SendCommandAsync(deviceId, commandName);
            IsDeviceConnected = isToggled;  // Only update if successful
            await _deviceManager.ReportLockStatusAsync();
        }
        catch (Microsoft.Azure.Devices.Common.Exceptions.DeviceNotFoundException)
        {
            IsDeviceConnected = false; // Reset to off if not successful
            ConnectionStatusText = "Device Not Connected";
            IsConnectionStatusVisible = true;

            await Task.Delay(3000);

            IsConnectionStatusVisible = false;
        }
    }
}
