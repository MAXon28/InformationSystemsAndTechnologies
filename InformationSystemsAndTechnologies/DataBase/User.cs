using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace InformationSystemsAndTechnologies.DataBase
{
    [Table("Users")]
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string NumberPhone { get; set; }

        public string Email { get; set; }

        public ICollection<History> Histories { get; set; }

        public User()
        {
            Histories = new List<History>();
        }
    }
}