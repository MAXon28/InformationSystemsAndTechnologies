using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using InformationSystemsAndTechnologies.Model;
using InformationSystemsAndTechnologies.View;

namespace InformationSystemsAndTechnologies.ViewModel
{
    class MainViewModel : ViewModelBase
    {
        private StaffsLogic staffsLogic;
        private UsersLogic usersLogic;

        public MainViewModel()
        {
            staffsLogic = StaffsLogic.GetInstance();
            usersLogic = UsersLogic.GetInstance();
            Persons = new List<string>
            {
                "Директор",
                "Персонал интернет-магазина",
                "Администратор интернет-магазина",
                "Пользователь"
            };
            Login = "";
            Password = "";
        }

        public List<string> Persons { get; set; }

        public string SelectedPerson { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public ICommand Registration
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MarketPage = new NewUserView();
                    MarketPage.DataContext = new NewUserViewModel();
                });
            }
        }

        public ICommand Input
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (staffsLogic != null && staffsLogic.IsTrueData(SelectedPerson, Login, Password))
                    {
                        LoadPage(StaffsLogic.GetRights());
                        SelectedPerson = "";
                        Login = "";
                        Password = "";
                    }
                    else if (usersLogic != null && SelectedPerson == "Пользователь" && usersLogic.IsTrueData(Login, Password))
                    {
                        LoadPage();
                        SelectedPerson = "";
                        Login = "";
                        Password = "";
                    }
                    else
                    {
                        MessageBox.Show("Неверный пароль или логин! Повторите попытку!", "Ошибка входа");
                    }
                });
            }
        }

        public FrameworkElement MarketPage { get; set; }

        private void LoadPage(int rights = 0)
        {
            switch (rights)
            {
                case 0:
                    MarketPage = new MarketPageUserView();
                    MarketPage.DataContext = new MarketPageUserViewModel();
                    break;
                case 1:
                    MarketPage = new MarketPagePersonalView();
                    MarketPage.DataContext = new MarketPagePersonalViewModel();
                    break;
                case 2:
                    MarketPage = new MarketPageAdminView();
                    MarketPage.DataContext = new MarketPageAdminViewModel(staffsLogic);
                    break;
                case 3:
                    MarketPage = new MarketPageDirectorView();
                    MarketPage.DataContext = new MarketPageDirectorViewModel(staffsLogic);
                    break;
            }
        }
    }
}