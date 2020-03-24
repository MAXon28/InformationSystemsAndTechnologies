using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using InformationSystemsAndTechnologies.Context;
using InformationSystemsAndTechnologies.DataBase;

namespace InformationSystemsAndTechnologies.Model
{
    class UsersLogic : ViewModelBase
    {
        private static UsersLogic usersLogic;
        private InternetMarketContext internetMarketContext;
        private static List<User> users;
        private static User userInput;

        private UsersLogic()
        {
            internetMarketContext = new InternetMarketContext();
            if (!internetMarketContext.Users.Any())
            {
                AddStartUsersAsync();
            }
            else
            {
                LoadAsync();
            }
        }

        public bool IsTrueData(string login, string password)
        {
            var usersForThisParameters = (from human in users
                                           where (human.Login == login) && (human.Password == password)
                select human).ToList();
            userInput = usersForThisParameters.Count != 0 ? usersForThisParameters[0] : null;
            return userInput != null;
        }

        public string GetFullName()
        {
            return userInput.Name + " " + userInput.Surname;
        }

        public string GetLogin()
        {
            return userInput.Login;
        }

        public string GetPassword()
        {
            return userInput.Password;
        }

        public string GetName()
        {
            return userInput.Name;
        }

        public string GetSurname()
        {
            return userInput.Surname;
        }

        public string GetNumberPhone()
        {
            return userInput.NumberPhone;
        }

        public string GetEmail()
        {
            return userInput.Email;
        }

        public void AddUser(string login, string password, string name, string surname, string numberPhone, string email)
        {
            User user = new User();
            user.Login = login;
            user.Password = password;
            user.Name = name;
            user.Surname = surname;
            user.NumberPhone = numberPhone;
            user.Email = email;
            users.Add(user);
            Task.Run(() => AddUserDb(user));
        }

        public void UpdateUser(string login, string password, string name, string surname, string numberPhone, string email)
        {
            int index = GetIndexById(GetId());
            users[index].Login = login;
            users[index].Password = password;
            users[index].Name = name;
            users[index].Surname = surname;
            users[index].NumberPhone = numberPhone;
            users[index].Email = email;
            userInput = users[index];
            Task.Run(() => UpdateUserDb(users[index]));
        }

        public bool IsUniqueLogin(string text)
        {
            foreach (var user in users)
            {
                if (user.Login == text)
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsUniquePassword(string text)
        {
            foreach (var user in users)
            {
                if (user.Password == text)
                {
                    return false;
                }
            }
            return true;
        }

        public static int GetId()
        {
            return userInput.Id;
        }

        public static UsersLogic GetInstance()
        {
            if (usersLogic == null)
            {
                usersLogic = new UsersLogic();
            }
            return usersLogic;
        }

        private void AddUserDb(User user)
        {
            internetMarketContext.Users.Add(user);
            internetMarketContext.SaveChanges();
        }

        private void UpdateUserDb(User userRewrite)
        {
            User user = internetMarketContext.Users.Find(GetId());
            user = userRewrite;
            internetMarketContext.SaveChanges();
        }

        private int GetIndexById(int id)
        {
            int index = 0;
            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        private async void LoadAsync()
        {
            await Task.Run(() => { users = internetMarketContext.Users.ToList(); });
        }

        private async void AddStartUsersAsync()
        {
            await Task.Run(() =>
            {
                internetMarketContext.Users.Add(new User
                {
                    Login = "Стельмакс99",
                    Password = "Стельмач",
                    Name = "Полина",
                    Surname = "Стельмах",
                    NumberPhone = "+79774594575",
                    Email = "stelmakh99@gmail.com"
                });

                internetMarketContext.Users.Add(new User
                {
                    Login = "Тест0",
                    Password = "1234",
                    Name = "Тест",
                    Surname = "Тестовый",
                    NumberPhone = "+79160000000",
                    Email = "test@gmail.com"
                });

                
                internetMarketContext.SaveChanges();
                users = internetMarketContext.Users.ToList();
            });
        }
    }
}