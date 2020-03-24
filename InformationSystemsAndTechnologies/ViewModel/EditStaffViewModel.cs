using System.Collections.Generic;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InformationSystemsAndTechnologies.Model;

namespace InformationSystemsAndTechnologies.ViewModel
{
    class EditStaffViewModel : ViewModelBase
    {
        private StaffsLogic staffsLogic;
        private int id;
        private string startName;
        private string startSurname;
        private string startPosition;

        public EditStaffViewModel() { }

        public EditStaffViewModel(StaffsLogic staffsLogic, int id)
        {
            this.staffsLogic = staffsLogic;
            this.id = id;
            Condition = "Visible";

            Positions = new List<string>
            {
                "Директор",
                "Персонал интернет-магазина",
                "Администратор интернет-магазина"
            };

            Name = StaffsLogic.GetName(id);
            Surname = StaffsLogic.GetSurname(id);
            Position = StaffsLogic.GetPosition(id);

            startName = Name;
            startSurname = Surname;
            startPosition = Position;
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
                    if (Name != startName || Surname != startSurname || Position != startPosition)
                    {
                        staffsLogic.UpdateStaff(id, Name, Surname, Position);
                        Condition = "Collapsed";
                    }
                });
            }
        }
    }
}