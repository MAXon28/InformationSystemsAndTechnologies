using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InformationSystemsAndTechnologies.Model;
using InformationSystemsAndTechnologies.View;

namespace InformationSystemsAndTechnologies.ViewModel
{
    class ReportPageViewModel : ViewModelBase
    {
        private ReportsLogic reportsLogic;

        public ReportPageViewModel() { }

        public ReportPageViewModel(string fullNameStaff)
        {
            reportsLogic = ReportsLogic.GetInstance();
            (List<string>, List<string>) readyLists = reportsLogic.GetListsForReport();
            Condition = "Visible";
            ZeroProducts = new ObservableCollection<string>(readyLists.Item1);
            SmallCountProducts = new ObservableCollection<string>(readyLists.Item2);
            TextReport = $"С уважением, {fullNameStaff}, компания \"П. А. Е. М.\"!";
        }

        public string Condition { get; set; }

        public ObservableCollection<string> ZeroProducts { get; set; }

        public ObservableCollection<string> SmallCountProducts { get; set; }

        public string TextReport { get; set; }

        public ICommand SendReport
        {
            get
            {
                return new RelayCommand(() =>
                {
                    reportsLogic.DeleteProductsAsync();
                    MessageBox.Show("Отчёт поставщику отправлен!", "Информация");
                    Condition = "Collapsed";
                });
            }
        }

        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(() =>
                {
                    Condition = "Collapsed";
                });
            }
        }
    }
}