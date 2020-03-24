using System;
using System.Windows;
using InformationSystemsAndTechnologies.ViewModel.Dialog;

namespace InformationSystemsAndTechnologies.View.Dialog
{
    public partial class RewriteRightsViewDialog : Window
    {
        public RewriteRightsViewDialog(RewriteRightsViewModelDialog viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}