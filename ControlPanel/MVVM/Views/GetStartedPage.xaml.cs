using ControlPanel.MVVM.ViewModels;
using SmartLibrary.MVVM.Models;
using System.Runtime.InteropServices;

namespace ControlPanel.Views;

public partial class GetStartedPage : ContentPage
{
	public GetStartedPage(GetStartedViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    private void OnSwitchToggled(object sender, ToggledEventArgs e)
    {
        var viewModel = (GetStartedViewModel)BindingContext;
        viewModel.ToggleState(e);
    }
}