using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InformationSystemsAndTechnologies.Model;
using InformationSystemsAndTechnologies.SideClasses;
using InformationSystemsAndTechnologies.View.Dialog;
using InformationSystemsAndTechnologies.ViewModel.Dialog;

namespace InformationSystemsAndTechnologies.ViewModel
{
    class OrdersPageViewModel : ViewModelBase
    {
        private HistoriesLogic historiesLogic;
        private string textSearch;

        public OrdersPageViewModel()
        {
            historiesLogic = HistoriesLogic.GetInstance();
            Condition = "Visible";
            Checks = new ObservableCollection<CheckForPersonal>(historiesLogic.GetChecksForPersonal());
            Count = Checks.Count;
            SeeCheck = new RelayCommand<int>(ThisCheck);
        }

        public string Condition { get; set; }

        public ObservableCollection<CheckForPersonal> Checks { get; set; }

        public int Count { get; set; }

        public string TextSearch
        {
            get { return textSearch; }
            set
            {
                if (value != null)
                {
                    textSearch = value;
                    if (IsText(textSearch))
                    {
                        Checks = new ObservableCollection<CheckForPersonal>(historiesLogic.GetSearchResponse(textSearch));
                    }
                    else
                    {
                        Checks = new ObservableCollection<CheckForPersonal>(historiesLogic.GetSearchResponse(Convert.ToInt32(textSearch)));
                    }
                    Count = Checks.Count;
                }
            }
        }

        public RelayCommand<int> SeeCheck { get; set; }

        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(() => { Condition = "Collapsed"; });
            }
        }

        private void ThisCheck(int id)
        {
            AboutCheckViewDialog aboutCheckViewDialog = new AboutCheckViewDialog(new AboutCheckViewModelDialog(id));
            aboutCheckViewDialog.ShowDialog();
        }

        private bool IsText(string txt)
        {
            bool isTrue;
            if (txt.Length > 0)
            {
                isTrue = (int) txt[0] < 48 || (int) txt[0] > 57;
            }
            else
            {
                return true;
            }
            if (txt.Length > 10)
            {
                return true;
            }
            for (int i = 1; i < txt.Length; i++)
            {
                if ((int) txt[i] < 48 || (int) txt[i] > 57 != isTrue)
                {
                    return true;
                }
            }
            return isTrue;
        }

    }
}