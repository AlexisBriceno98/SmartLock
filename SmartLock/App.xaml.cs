using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SmartLibrary.MVVM.Models;
using SmartLibrary.Services;
using System.Windows;

namespace SmartLock
{
    public partial class App : Application
    {
        public static IHost? host { get; set; }

        public App()
        {
            host = Host.CreateDefaultBuilder().ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((config, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<NetworkManager>();
                services.AddSingleton<DeviceManager>();

                var connectionString = config.Configuration.GetSection("IoTHub:ConnectionString").Value;
                services.AddSingleton(new DeviceConfigurationModel(config.Configuration.GetConnectionString("Device")!));

                services.AddSingleton<IotHubService>();

            }).Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host!.StartAsync();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

    }
}
