using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.Azure.Devices.Client;
using System.Text;
using SmartLibrary.Services;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Diagnostics;

namespace SmartLock;

public partial class MainWindow : Window
{
    private readonly NetworkManager _networkManager;
    private readonly DeviceManager _deviceManager;
    public MainWindow(NetworkManager networkManager, DeviceManager deviceManager)
    {
        InitializeComponent();
        _networkManager = networkManager;
        _deviceManager = deviceManager;

        // Subscribe to the event
        _deviceManager.OnCommandReceived += (command) =>
        {
            Debug.WriteLine($"Command received: {command}");
            UpdateLockState();
        };

        // Only start the CheckConnectivityAsync task on initialization
        Task.Run(CheckConnectivityAsync);
    }
    private void UpdateLockState()
    {
        this.Dispatcher.Invoke(() =>
        {
            Storyboard lockStoryboard = (Storyboard)this.FindResource("LockChangeStoryBoard");

            if (_deviceManager.AllowSending())
            {
                lockStoryboard.Begin();
            }
            else
            {
                LockIcon.Foreground = new SolidColorBrush(Colors.Black);
            }
        });
    }

    private async Task CheckConnectivityAsync()
    {
        while (true)
        {
            ConnectivityStatus.Text = await _networkManager.CheckConnectivityAsync();
            await Task.Delay(1000);
        }
    }
}
