using ControlPanel.MVVM.ViewModels;

namespace ControlPanel.Views;

public partial class DeviceListPage : ContentPage
{
	public DeviceListPage(DeviceListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}