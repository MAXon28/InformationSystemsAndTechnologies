using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InformationSystemsAndTechnologies.Model;

namespace InformationSystemsAndTechnologies.ViewModel
{
    class NewStaffViewModel : ViewModelBase
    {
        private StaffsLogic staffsLogic;

        public NewStaffViewModel() { }

        public NewStaffViewModel(StaffsLogic staffsLogic)
        {
            this.staffsLogic = staffsLogic;
            Condition = "Visible";
            Positions = new List<string>
            {
                "Директор",
                "Персонал интернет-магазина",
                "Администратор интернет-магазина"
            }; ;
        }

        public string Condition { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Position { get; set; }

        public List<string> Positions { get; set; }

        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(() => Condition = "Collapsed");
            }
        }

        public ICommand SaveNewStaff
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (Name != null && Surname != null && Position != null)
                    {
                        staffsLogic.AddStaff(Name, Surname, Position);
                        Condition = "Collapsed";
                    }
                });
            }
        }
    }
}
