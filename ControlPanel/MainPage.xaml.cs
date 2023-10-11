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
    }
}