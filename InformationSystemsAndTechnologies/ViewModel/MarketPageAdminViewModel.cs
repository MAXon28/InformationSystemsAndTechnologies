using System.Collections.ObjectModel;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InformationSystemsAndTechnologies.DataBase;
using InformationSystemsAndTechnologies.Model;
using InformationSystemsAndTechnologies.View.Dialog;
using InformationSystemsAndTechnologies.ViewModel.Dialog;

namespace InformationSystemsAndTechnologies.ViewModel
{
    class MarketPageAdminViewModel : ViewModelBase
    {
        private StaffsLogic staffsLogic;
        private RewriteLoginViewModelDialog rewriteLoginViewModelDialog;
        private RewriteLoginViewDialog rewriteLoginViewDialog;
        private RewritePasswordViewModelDialog rewritePasswordViewModelDialog;
        private RewritePasswordViewDialog rewritePasswordViewDialog;
        private RewriteRightsViewModelDialog rewriteRightsViewModelDialog;
        private RewriteRightsViewDialog rewriteRightsViewDialog;

        public MarketPageAdminViewModel() { }

        public MarketPageAdminViewModel(StaffsLogic staffsLogic)
        {
            this.staffsLogic = new StaffsLogic();
            Staffs = new ObservableCollection<Staff>(StaffsLogic.GetList());
            Condition = "Visible";
            Position = StaffsLogic.GetPosition();
            FirstName = StaffsLogic.GetName();
            LastName = StaffsLogic.GetSurname();
            RewriteLogin = new RelayCommand<int>(UpdateLogin);
            RewritePassword = new RelayCommand<int>(UpdatePassword);
            RewriteRights = new RelayCommand<int>(UpdateRights);
        }

        public ObservableCollection<Staff> Staffs { get; set; }

        public string Condition { get; set; }

        public string Position { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(() => Condition = "Collapsed");
            }
        }

        public RelayCommand<int> RewriteLogin { get; set; }

        public RelayCommand<int> RewritePassword { get; set; }

        public RelayCommand<int> RewriteRights { get; set; }

        private void UpdateLogin(int id)
        {
            rewriteLoginViewModelDialog = new RewriteLoginViewModelDialog(staffsLogic, id);
            rewriteLoginViewDialog = new RewriteLoginViewDialog(rewriteLoginViewModelDialog);
            rewriteLoginViewDialog.ShowDialog();
            Staffs = new ObservableCollection<Staff>(StaffsLogic.GetList());
        }

        private void UpdatePassword(int id)
        {
            rewritePasswordViewModelDialog = new RewritePasswordViewModelDialog(staffsLogic, id);
            rewritePasswordViewDialog = new RewritePasswordViewDialog(rewritePasswordViewModelDialog);
            rewritePasswordViewDialog.ShowDialog();
            Staffs = new ObservableCollection<Staff>(StaffsLogic.GetList());
        }

        private void UpdateRights(int id)
        {
            rewriteRightsViewModelDialog = new RewriteRightsViewModelDialog(staffsLogic, id);
            rewriteRightsViewDialog = new RewriteRightsViewDialog(rewriteRightsViewModelDialog);
            rewriteRightsViewDialog.ShowDialog();
            Staffs = new ObservableCollection<Staff>(StaffsLogic.GetList());
        }
    }
}