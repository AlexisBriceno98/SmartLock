using CommunityToolkit.Maui;
using ControlPanel.Views;
using ControlPanel.MVVM.ViewModels;
using ControlPanel.MVVM.Views;

namespace ControlPanel
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("Rubik-Regular.ttf", "RubikRegular");
                    fonts.AddFont("fa-thin-100.ttf", "FontAwesomeThin");
                    fonts.AddFont("fa-light-300.ttf", "FontAwesomeLight");
                    fonts.AddFont("fa-regular-400.ttf", "FontAwesomeRegular");
                    fonts.AddFont("fa-solid-900.ttf", "FontAwesomeSolid");
                });
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<MainPage>();

            builder.Services.AddSingleton<GetStartedPage>();
            builder.Services.AddSingleton<GetStartedViewModel>();

            builder.Services.AddSingleton<DateAndTimePage>();
            builder.Services.AddSingleton<DateAndTimeViewModel>();

            builder.Services.AddSingleton<WeatherInfoPage>();
            builder.Services.AddSingleton<WeatherInfoViewModel>();

            builder.Services.AddSingleton<DeviceListPage>();
            builder.Services.AddSingleton<DeviceListViewModel>();

            builder.Services.AddSingleton<SettingsPage>();
            builder.Services.AddSingleton<SettingsViewModel>();

            builder.Services.AddSingleton<RegisterDevicePage>();
            builder.Services.AddSingleton<RegisterDeviceViewModel>();

            return builder.Build();
        }
    }
}