using ControlPanel.MVVM.ViewModels;
using ControlPanel.Views;

namespace ControlPanel
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = BindingContext as MainViewModel;
            await viewModel?.CheckConfiguration();
        }
    }
}