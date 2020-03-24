using System.Windows;
using InformationSystemsAndTechnologies.ViewModel.Dialog;

namespace InformationSystemsAndTechnologies.View.Dialog
{
    public partial class RewritePasswordViewDialog : Window
    {
        public RewritePasswordViewDialog(RewritePasswordViewModelDialog viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}