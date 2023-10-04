using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.Azure.Devices.Client;
using System.Text;
using SmartLibrary.Services;
using System.Threading.Tasks;

namespace SmartLock;

public partial class MainWindow : Window
{
    private readonly IotHubService _iotHubService;
    private readonly DeviceManager _deviceManager;
    public MainWindow(IotHubService iotHubService, DeviceManager deviceManager)
    {
        InitializeComponent();
        _iotHubService = iotHubService;
        _deviceManager = deviceManager;
        _deviceManager.OnCommandReceived += CommandReceived;
    }

    private async void ToggleLockButton_Click(object sender, RoutedEventArgs e)
    {

        if (LockIcon.Icon == FontAwesome.WPF.FontAwesomeIcon.Lock)
        {
            LockIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.Unlock;
            Storyboard colorChangeStoryboard = this.FindResource("ColorChangesStoryBoard") as Storyboard;
            colorChangeStoryboard.Begin();
            DeviceStatusTextBlock.Text = "Lock Status: Unlocked";
        }
        else
        {
            LockIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.Lock;
            DeviceStatusTextBlock.Text = "Lock Status: Locked";
        }

        await _deviceManager.SendTelemetryAsync();
    }

    private void CommandReceived(string command)
    {
        this.Dispatcher.Invoke(() =>
        {
            if (command == "unlock")
            {
                LockIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.Unlock;
                Storyboard colorChangeStoryboard = this.FindResource("ColorChangesStoryBoard") as Storyboard;
                colorChangeStoryboard.Begin();
                DeviceStatusTextBlock.Text = "Lock Status: Unlocked";
            }
            else if (command == "lock")
            {
                LockIcon.Icon = FontAwesome.WPF.FontAwesomeIcon.Lock;
                DeviceStatusTextBlock.Text = "Lock Status: Locked";
            }
        });
    }
}
