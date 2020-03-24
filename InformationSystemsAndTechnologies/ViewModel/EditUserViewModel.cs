using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InformationSystemsAndTechnologies.Model;

namespace InformationSystemsAndTechnologies.ViewModel
{
    public class EditUserViewModel : ViewModelBase
    {
        private UsersLogic usersLogic;
        private string startLogin;
        private string startPassword;
        private string startName;
        private string startSurname;
        private string startPhoneNumber;
        private string startEmail;

        public EditUserViewModel()
        {
            usersLogic = UsersLogic.GetInstance();
            Condition = "Visible";
            Login = usersLogic.GetLogin();
            Password = usersLogic.GetPassword();
            Name = usersLogic.GetName();
            Surname = usersLogic.GetSurname();
            PhoneNumber = usersLogic.GetNumberPhone();
            Email = usersLogic.GetEmail();

            startLogin = Login;
            startPassword = Password;
            startName = Name;
            startSurname = Surname;
            startPhoneNumber = PhoneNumber;
            startEmail = Email;
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
                    if (Login != startLogin || Password != startPassword ||Name != startName || Surname != startSurname || PhoneNumber != startPhoneNumber || Email != startEmail)
                    {
                        var isUniqueLogin = usersLogic.IsUniqueLogin(Login);
                        var isUniquePassword = usersLogic.IsUniquePassword(Password);
                        if (!isUniqueLogin && Login != startLogin)
                        {
                            MessageBox.Show("Данный логин уже занят!", "Ошибка");
                        }
                        else if (!isUniquePassword && Password != startPassword)
                        {
                            MessageBox.Show("Данный пароль не уникален!", "Ошибка");
                        }
                        else
                        {
                            usersLogic.UpdateUser(Login, Password, Name, Surname, PhoneNumber, Email);
                            Condition = "Collapsed";
                        }
                    }
                });
            }
        }
    }
}