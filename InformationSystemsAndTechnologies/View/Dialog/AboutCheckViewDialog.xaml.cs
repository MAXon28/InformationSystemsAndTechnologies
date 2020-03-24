using System.Windows;
using InformationSystemsAndTechnologies.ViewModel.Dialog;

namespace InformationSystemsAndTechnologies.View.Dialog
{
    public partial class AboutCheckViewDialog : Window
    {
        public AboutCheckViewDialog(AboutCheckViewModelDialog viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}