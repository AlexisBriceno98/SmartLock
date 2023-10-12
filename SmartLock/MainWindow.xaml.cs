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
        if (LockIcon.Text == "\uf023")
        {
            LockIcon.Text = "\uf09c";
            Storyboard unlockStoryboard = (Storyboard)this.FindResource("UnlockColorChangeStoryBoard");
            unlockStoryboard.Begin();
            DeviceStatusTextBlock.Text = "Lock Status: Unlocked";
        }
        else
        {
            LockIcon.Text = "\uf023";
            Storyboard lockStoryboard = (Storyboard)this.FindResource("LockColorChangeStoryBoard");
            lockStoryboard.Begin();
            DeviceStatusTextBlock.Text = "Lock Status: Locked";
        }

        //await _deviceManager.SendTelemetryAsync();
        //await _deviceManager.ReportLockStatusAsync();
    }

    private async void CommandReceived(string command)
    {
        await this.Dispatcher.InvokeAsync(async () =>
        {
            if (command == "unlock")
            {
                LockIcon.Text = "\uf09c";
                Storyboard unlockStoryboard = (Storyboard)this.FindResource("UnlockColorChangeStoryBoard");
                unlockStoryboard.Begin();
                DeviceStatusTextBlock.Text = "Lock Status: Unlocked";
            }
            else if (command == "lock")
            {
                LockIcon.Text = "\uf023";
                Storyboard lockStoryboard = (Storyboard)this.FindResource("LockColorChangeStoryBoard");
                lockStoryboard.Begin();
                DeviceStatusTextBlock.Text = "Lock Status: Locked";
            }
            //await _deviceManager.SendTelemetryAsync();
            //await _deviceManager.ReportLockStatusAsync();
        });
    }
}
