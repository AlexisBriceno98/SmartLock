using ControlPanel.MVVM.Models;
using ControlPanel.MVVM.ViewModels;
using System.Runtime.InteropServices;

namespace ControlPanel.Views;

public partial class GetStartedPage : ContentPage
{
	public GetStartedPage(GetStartedViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
		var _switch = (Switch)sender;
		var iotDevice = _switch.BindingContext as IotDevice;

		var viewModel = BindingContext as GetStartedViewModel;
        if (iotDevice == null)
        {
            viewModel.ToggleStateCommand.Execute(iotDevice);
        }

    }
}