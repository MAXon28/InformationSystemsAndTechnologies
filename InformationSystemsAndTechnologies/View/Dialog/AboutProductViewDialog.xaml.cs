using System.Windows;
using InformationSystemsAndTechnologies.ViewModel.Dialog;

namespace InformationSystemsAndTechnologies.View.Dialog
{
    public partial class AboutProductViewDialog : Window
    {
        public AboutProductViewDialog(AboutProductViewModelDialog viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}