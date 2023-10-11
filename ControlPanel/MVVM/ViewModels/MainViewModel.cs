using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ControlPanel.MVVM.Views;
using ControlPanel.Views;

namespace ControlPanel.MVVM.ViewModels;

public partial class MainViewModel : ObservableObject
{

    [RelayCommand]
    async Task GoToGetStarted()
    {
        await Shell.Current.GoToAsync(nameof(GetStartedPage));
    }

    [RelayCommand]
    async Task GoToSettings()
    {
        await Shell.Current.GoToAsync(nameof(SettingsPage));
    }

    [ObservableProperty]
    private bool isConfigured;
    public async Task CheckConfiguration()
    {
        if (!isConfigured)
        {
            await Shell.Current.GoToAsync(nameof(RegisterDevicePage));
        }
    }
}
