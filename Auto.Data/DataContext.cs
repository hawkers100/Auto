using System.Data.Entity;
using Auto.Data.Migrations;
using Auto.Data.Models;

namespace Auto.Data
{

    public class DataContext : DbContext
    {
        public DataContext()
            : base("name=AutoDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>());
        }

        public DbSet<CarRent> CarRents { get; set; }
    }
}
