namespace ControlPanel.Views;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
	}

	private void OnGetStartedClicked(object sender, EventArgs e)
	{
		Navigation.PushAsync(new GetStartedPage());
	}

    private void OnSettingsClicked(object sender, EventArgs e)
    {
        Navigation.PushAsync(new SettingsPage());
    }
}