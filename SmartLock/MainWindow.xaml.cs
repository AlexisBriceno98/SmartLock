using System.Windows;
using System.Windows.Media.Animation;
using Microsoft.Azure.Devices.Client;
using System.Text;

namespace SmartLock;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void ToggleLockButton_Click(object sender, RoutedEventArgs e)
    {

        Storyboard scaleStoryboard = this.FindResource("ScaleStoryBoard") as Storyboard;
        scaleStoryboard.Begin();

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
    }
}
