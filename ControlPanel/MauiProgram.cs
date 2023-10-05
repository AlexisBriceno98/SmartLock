using CommunityToolkit.Maui;
using ControlPanel.Views;
using SmartLibrary.MVVM.ViewModels;

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
                });

            builder.Services.AddSingleton<WelcomePage>();
            builder.Services.AddSingleton<WelcomeViewModel>();

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

            return builder.Build();
        }
    }
}