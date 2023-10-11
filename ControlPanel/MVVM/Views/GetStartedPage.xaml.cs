using ControlPanel.MVVM.ViewModels;

namespace ControlPanel.Views;

public partial class GetStartedPage : ContentPage
{
	public GetStartedPage()
	{
		InitializeComponent();
		BindingContext = new GetStartedViewModel();
	}
}