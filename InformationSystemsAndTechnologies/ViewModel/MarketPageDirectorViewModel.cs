using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InformationSystemsAndTechnologies.DataBase;
using InformationSystemsAndTechnologies.Model;
using InformationSystemsAndTechnologies.View;

namespace InformationSystemsAndTechnologies.ViewModel
{
    class MarketPageDirectorViewModel : ViewModelBase
    {
        private StaffsLogic staffsLogic;
        private NewStaffViewModel newStaffViewModel;
        private EditStaffViewModel editStaffViewModel;

        public MarketPageDirectorViewModel() { }

        public MarketPageDirectorViewModel(StaffsLogic staffsLogic)
        {
            this.staffsLogic = staffsLogic;
            Staffs = new ObservableCollection<Staff>(StaffsLogic.GetList());
            Condition = "Visible";
            Position = StaffsLogic.GetPosition();
            FirstName = StaffsLogic.GetName();
            LastName = StaffsLogic.GetSurname();
            UpdateStaff = new RelayCommand<int>(Update);
            DeleteStaff = new RelayCommand<int>(Delete);
        }

        public ObservableCollection<Staff> Staffs { get; set; }

        public string Condition { get; set; }

        public string Position { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public ICommand Add
        {
            get
            {
                return new RelayCommand(() =>
                {
                    newStaffViewModel = new NewStaffViewModel(staffsLogic);
                    AddOrEditStaff = new NewStaffView();
                    AddOrEditStaff.DataContext = newStaffViewModel;
                    WaitingAdd();
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

        public RelayCommand<int> UpdateStaff { get; set; }

        public RelayCommand<int> DeleteStaff { get; set; }

        public FrameworkElement AddOrEditStaff { get; set; }

        private void Update(int id)
        {
            editStaffViewModel = new EditStaffViewModel(staffsLogic, id);
            AddOrEditStaff = new EditStaffView();
            AddOrEditStaff.DataContext = editStaffViewModel;
            WaitingEdit();
        }

        private void Delete(int id)
        {
            if (MessageBox.Show("Вы уверены, что хотите уволить этого сотрудника?", "Увольнение сотрудника", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                Staffs.Remove(GetElementById(id));
                staffsLogic.DeleteStaffAsync(id);
            }
        }

        private Staff GetElementById(int id)
        {
            IEnumerator<Staff> iterator = Staffs.GetEnumerator();
            while (iterator.MoveNext())
            {
                if (iterator.Current.Id == id)
                {
                    return iterator.Current;
                }
            }
            return null;
        }

        private void WaitingAdd()
        {
            Task.Run(() =>
            {
                while (newStaffViewModel.Condition == "Visible") ;
                Staffs = new ObservableCollection<Staff>(StaffsLogic.GetList());
            });
        }

        private void WaitingEdit()
        {
            Task.Run(() =>
            {
                while (editStaffViewModel.Condition == "Visible") ;
                Staffs = new ObservableCollection<Staff>(StaffsLogic.GetList());
            });
        }
    }
}