using ControlPanel.MVVM.Views;
using ControlPanel.Views;

namespace ControlPanel
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(GetStartedPage), typeof(GetStartedPage));
            Routing.RegisterRoute(nameof(DeviceListPage), typeof(DeviceListPage));
            Routing.RegisterRoute(nameof(RegisterDevicePage), typeof(RegisterDevicePage));
        }
    }
}