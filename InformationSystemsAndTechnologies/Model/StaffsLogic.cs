using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using InformationSystemsAndTechnologies.Context;
using InformationSystemsAndTechnologies.DataBase;

namespace InformationSystemsAndTechnologies.Model
{
    public class StaffsLogic : ViewModelBase
    {
        private static StaffsLogic staffsLogic;
        private readonly InternetMarketContext internetMarketContext;
        private static List<Staff> staffs;
        private static Staff staffInput;
        private static Dictionary<int, Staff> staffsTable;

        public StaffsLogic()
        {
            internetMarketContext = new InternetMarketContext();
            if (!internetMarketContext.Staffs.Any())
            {
                AddStartStaffs();
            }
            else
            {
                LoadStaffs();
            }
        }

        public bool IsTrueData(string position, string login, string password)
        {
            var staffsForThisParameters = (from human in staffs
                                       where (human.Position == position) && (human.Login == login) && (human.Password == password)
                                       select human).ToList();
            staffInput = staffsForThisParameters.Count != 0 ? staffsForThisParameters[0] : null;
            return staffInput != null;
        }

        public void AddStaff(string name, string surname, string position)
        {
            Staff staff = new Staff();
            staff.Name = name;
            staff.Surname = surname;
            staff.Position = position;
            AddStaffDb(staff);
        }


        public async void UpdateStaff(int id, string name, string surname, string position)
        {
            int index = GetIndexById(id);
            staffs[index].Name = name;
            staffs[index].Surname = surname;
            staffs[index].Position = position;
            UpdateTableNameSurnamePosition(id, name, surname, position);
            await Task.Run(() => UpdateTableNameSurnamePositionDb(id, name, surname, position));
        }


        public async void UpdateLoginAsync(int id, string login)
        {
            staffs[GetIndexById(id)].Login = login;
            UpdateTableLogin(id, login);
            await Task.Run(() => UpdateLoginDb(id, login));
        }

        public async void UpdatePasswordAsync(int id, string password)
        {
            staffs[GetIndexById(id)].Password = password;
            UpdateTablePassword(id, password);
            await Task.Run(() => UpdatePasswordDb(id, password));
        }

        public async void UpdateRightsAsync(int id, int rights)
        {
            staffs[GetIndexById(id)].Rights = rights;
            UpdateTableRights(id, rights);
            await Task.Run(() => UpdateRightsDb(id, rights));
        }


        public async void DeleteStaffAsync(int id)
        {
            staffs.RemoveAt(GetIndexById(id));
            DeleteTableStaff(id);
            await Task.Run(() => DeleteStaffDb(id));
        }


        public static string GetName()
        {
            return staffInput.Name;
        }

        public static StaffsLogic GetInstance()
        {
            if (staffsLogic == null)
            {
                staffsLogic = new StaffsLogic();
            }
            return staffsLogic;
        }

        public static string GetName(int key)
        {
            return staffsTable[key].Name;
        }

        public static string GetSurname()
        {
            return staffInput.Surname;
        }

        public static string GetSurname(int key)
        {
            return staffsTable[key].Surname;
        }

        public static string GetPosition()
        {
            return staffInput.Position;
        }

        public static string GetPosition(int key)
        {
            return staffsTable[key].Position;
        }

        public static int GetRights()
        {
            return staffInput.Rights;
        }

        public static List<Staff> GetList()
        {
            return staffs;
        }

        public static string GetLogin(int key)
        {
            return staffsTable[key].Login;
        }

        public static string GetPassword(int key)
        {
            return staffsTable[key].Password;
        }

        public static string GetRights(int key)
        {
            return staffsTable[key].Rights.ToString();
        }

        public static bool IsUniqueLogin(string login)
        {
            foreach (var staff in staffs)
            {
                if (staff.Login == login)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsUniquePassword(string password)
        {
            foreach (var staff in staffs)
            {
                if (staff.Password == password)
                {
                    return false;
                }
            }
            return true;
        }


        private void AddStaffDb(Staff staff)
        {
            internetMarketContext.Staffs.Add(staff);
            internetMarketContext.SaveChanges();
            staffs = internetMarketContext.Staffs.ToList();
            AllAddHashtableAsync();
        }


        private void UpdateTableNameSurnamePositionDb(int id, string name, string surname, string position)
        {
            Staff staff = internetMarketContext.Staffs.Find(id);
            staff.Name = name;
            staff.Surname = surname;
            staff.Position = position;
            internetMarketContext.SaveChanges();
        }

        private void UpdateLoginDb(int id, string text)
        {
            Staff staff = internetMarketContext.Staffs.Find(id);
            staff.Login = text;
            internetMarketContext.SaveChanges();
        }

        private void UpdatePasswordDb(int id, string text)
        {
            Staff staff = internetMarketContext.Staffs.Find(id);
            staff.Password = text;
            internetMarketContext.SaveChanges();
        }

        private void UpdateRightsDb(int id, int number)
        {
            Staff staff = internetMarketContext.Staffs.Find(id);
            staff.Rights = number;
            internetMarketContext.SaveChanges();
        }


        private void DeleteStaffDb(int id)
        {
            Staff staff = internetMarketContext.Staffs.Find(id);
            internetMarketContext.Staffs.Remove(staff);
            internetMarketContext.SaveChanges();
        }


        private void UpdateTableNameSurnamePosition(int id, string name, string surname, string position)
        {
            staffsTable[id].Name = name;
            staffsTable[id].Surname = surname;
            staffsTable[id].Position = position;
        }

        private void UpdateTableLogin(int id, string text)
        {
            staffsTable[id].Login = text;
        }

        private void UpdateTablePassword(int id, string text)
        {
            staffsTable[id].Password = text;
        }

        private void UpdateTableRights(int id, int number)
        {
            staffsTable[id].Rights = number;
        }


        private void DeleteTableStaff(int id)
        {
            staffsTable.Remove(id);
        }


        private int GetIndexById(int id)
        {
            int index = 0;
            foreach (var staff in staffs)
            {
                if (staff.Id == id)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }

        private void AddStartStaffs()
        {
            Task.Run(() =>
            {
                internetMarketContext.Staffs.Add(new Staff
                {
                    Name = "Евгения",
                    Surname = "Белькова",
                    Position = "Директор",
                    Login = "Манагер",
                    Password = "0000",
                    Rights = 3
                });
                internetMarketContext.Staffs.Add(new Staff
                {
                    Name = "Максим",
                    Surname = "Безуглый",
                    Position = "Администратор интернет-магазина",
                    Login = "Админушка",
                    Password = "1111",
                    Rights = 2
                });
                internetMarketContext.Staffs.Add(new Staff
                {
                    Name = "Антон",
                    Surname = "Ткачев",
                    Position = "Персонал интернет-магазина",
                    Login = "Интернетный кассир",
                    Password = "2222",
                    Rights = 1
                });

                internetMarketContext.SaveChanges();

                staffs = internetMarketContext.Staffs.ToList();
                AllAddHashtableAsync();
            });
        }

        private void LoadStaffs()
        {
            Task.Run(() =>
            {
                staffs = internetMarketContext.Staffs.ToList();
                AllAddHashtableAsync();
            });
        }

        private async void AllAddHashtableAsync()
        {
            await Task.Run(() =>
            {
                staffsTable = new Dictionary<int, Staff>();
                foreach (var staff in staffs)
                {
                    staffsTable.Add(staff.Id, staff);
                }
            });
        }
    }
}