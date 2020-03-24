using System;
using System.Collections.Generic;

namespace InformationSystemsAndTechnologies.DataBase
{
    public class History
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Price { get; set; }

        public int UserId { get; set; }

        public User user { get; set; }

        public ICollection<Order> Orders { get; set; }

        public History()
        {
            Orders = new List<Order>();
        }
    }
}