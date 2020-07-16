using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ZChangerMMO.DataModels
{
    public class SQLiteProfileDbContext : DbContext
    {
        public SQLiteProfileDbContext() : base("SQLiteProfileDbContext")
        {
            //Database.SetInitializer<ProfileDbContext>(null);
            //Database.SetInitializer<ProfileDbContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
                .Property(b => b._Proxies).HasColumnName("Proxies");
            // Database does not pluralize table names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public DbSet<Profile> Profiles { get; set; }
        public DbSet<BackupItem> BackupItems { get; set; }
        public DbSet<ActionLog> ActionLogs { get; set; }
    }
}
