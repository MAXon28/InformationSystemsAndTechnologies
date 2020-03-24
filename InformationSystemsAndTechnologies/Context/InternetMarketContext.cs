using System.Data.Entity;
using InformationSystemsAndTechnologies.DataBase;

namespace InformationSystemsAndTechnologies.Context
{
    public class InternetMarketContext : DbContext
    {
        public InternetMarketContext() : base("DBConnection") { }

        public DbSet<Staff> Staffs { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<History> Histories { get; set; }

        public DbSet<Order> Orders { get; set; }
    }
}