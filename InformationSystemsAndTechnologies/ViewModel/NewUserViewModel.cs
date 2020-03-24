using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using InformationSystemsAndTechnologies.Model;

namespace InformationSystemsAndTechnologies.ViewModel
{
    class NewUserViewModel : ViewModelBase
    {
        private UsersLogic usersLogic;

        public NewUserViewModel()
        {
            usersLogic = UsersLogic.GetInstance();
            Condition = "Visible";
            Login = "";
            Password = "";
            Name = "";
            Surname = "";
            PhoneNumber = "";
            Email = "";
        }

        public string Condition { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public ICommand Cancel
        {
            get
            {
                return new RelayCommand(() => Condition = "Collapsed");
            }
        }

        public ICommand SaveUser
        {
            get
            {
                return new GalaSoft.MvvmLight.Command.RelayCommand(() =>
                {
                    var isUniqueLogin = usersLogic.IsUniqueLogin(Login);
                    var isUniquePassword = usersLogic.IsUniquePassword(Password);
                    if (Login != "" && Password != "" && Name != "" && Surname != "" && PhoneNumber != "" && Email != "" && isUniqueLogin && isUniquePassword)
                    {
                        usersLogic.AddUser(Login, Password, Name, Surname, PhoneNumber, Email);
                        Condition = "Collapsed";
                    }
                    else
                    {
                        if (!isUniqueLogin)
                        {
                            MessageBox.Show("Данный логин уже занят!", "Ошибка");
                        }
                        else if (!isUniquePassword)
                        {
                            MessageBox.Show("Данный пароль не уникален!", "Ошибка");
                        }
                    }
                });
            }
        }
    }
}