using System;
using System.Collections.ObjectModel;
using System.Linq;
using GalaSoft.MvvmLight;
using InformationSystemsAndTechnologies.Model;
using InformationSystemsAndTechnologies.SideClasses;

namespace InformationSystemsAndTechnologies.ViewModel.Dialog
{
    public class AboutCheckViewModelDialog : ViewModelBase
    {
        private HistoriesLogic historiesLogic;

        public AboutCheckViewModelDialog() { }

        public AboutCheckViewModelDialog(int id)
        {
            historiesLogic = HistoriesLogic.GetInstance();
            Title = "Чек №" + id;
            Informations = new ObservableCollection<CheckInformation>(historiesLogic.GetCheckInformation(id));
            Date = CheckInformation.DateBuy.ToString("f");
            FinishSum = CheckInformation.FinishSum;
        }

        public string Title { get; set; }

        public string Date { get; set; }

        public ObservableCollection<CheckInformation> Informations { get; set; }

        public int FinishSum { get; set; }
    }
}