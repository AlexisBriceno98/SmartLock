using ControlPanel.MVVM.ViewModels;

namespace ControlPanel.MVVM.Views;

public partial class RegisterDevicePage : ContentPage
{
	public RegisterDevicePage(RegisterDeviceViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}