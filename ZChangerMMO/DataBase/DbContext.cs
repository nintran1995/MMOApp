using SQLite.CodeFirst;
using System.Data.Entity;

namespace ZChangerMMO.Models
{
    //[DbConfigurationType(typeof(ZChangerMMO.Common.DataModel.EntityFramework.MyDbSQLiteConfiguration))]
    public class MyDbContext : DbContext
    {
        public MyDbContext() : base("SQLiteProfileDbContext")
        {
            //Database.SetInitializer<MyDbContext>(null);
        }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<MyDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
