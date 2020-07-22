using SQLite.CodeFirst;
using System.Data.Entity;
using System.Linq;
using ZChangerMMO.Models;

namespace ZChangerMMO.Infrastructure
{
    public class ZChangerContext : DbContext
    {
        public ZChangerContext() : base("SQLiteProfileDbContext") { }

        public DbSet<Email> Emails { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<ZChangerContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
    }
}
