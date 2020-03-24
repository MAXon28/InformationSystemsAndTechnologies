using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InformationSystemsAndTechnologies.Model;
using InformationSystemsAndTechnologies.SideClasses;
using InformationSystemsAndTechnologies.View;
using InformationSystemsAndTechnologies.View.Dialog;
using InformationSystemsAndTechnologies.ViewModel.Dialog;

namespace InformationSystemsAndTechnologies.ViewModel
{
    class MarketPageUserViewModel : ViewModelBase
    {
        private UsersLogic usersLogic;
        private HistoriesLogic historiesLogic;
        private EditUserViewModel editUserViewModel;
        private OrderViewModel orderViewModel;

        public MarketPageUserViewModel()
        {
            usersLogic = UsersLogic.GetInstance();
            historiesLogic = HistoriesLogic.GetInstance();
            Condition = "Visible";
            FullName = usersLogic.GetFullName();
            Checks = new ObservableCollection<Check>(historiesLogic.GetChecks(UsersLogic.GetId()));
            SeeCheck = new RelayCommand<int>(ThisCheck);
            ProductsLogic.GetInstance();
        }

        public ObservableCollection<Check> Checks { get; set; }

        public string Condition { get; set; }

        public string FullName { get; set; }

        public RelayCommand<int> SeeCheck { get; set; }

        public ICommand PersonalData
        {
            get
            {
                return new RelayCommand(() =>
                {
                    editUserViewModel = new EditUserViewModel();
                    AnotherPage = new EditUserView();
                    AnotherPage.DataContext = editUserViewModel;
                    WaitingUpdate();
                });
            }
        }

        public ICommand MakeOrder
        {
            get
            {
                return new RelayCommand(() =>
                {
                    orderViewModel = new OrderViewModel();
                    AnotherPage = new OrderView();
                    AnotherPage.DataContext = orderViewModel;
                    WaitingOrder();
                });
            }
        }

        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(() => Condition = "Collapsed");
            }
        }

        public FrameworkElement AnotherPage { get; set; }

        private void ThisCheck(int id)
        {
            AboutCheckViewDialog aboutCheckViewDialog = new AboutCheckViewDialog(new AboutCheckViewModelDialog(id));
            aboutCheckViewDialog.ShowDialog();
        }

        private void WaitingUpdate()
        {
            Task.Run(() =>
            {
                while (editUserViewModel.Condition == "Visible") ;
                FullName = usersLogic.GetFullName();
            });
        }

        private void WaitingOrder()
        {
            Task.Run(() =>
            {
                while (orderViewModel.Condition == "Visible") ;
                Checks = new ObservableCollection<Check>(historiesLogic.GetChecks(UsersLogic.GetId()));
            });
        }
    }
}