using DevExpress.Internal;
using System;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;

namespace ZChangerMMO.Common.DataModel.EntityFramework
{
    public class SQLiteConnectionFactory : System.Data.Entity.Infrastructure.IDbConnectionFactory {
        static string filePath;
        public DbConnection CreateConnection(string nameOrConnectionString) {
            if(filePath == null) {
                filePath = DataDirectoryHelper.GetFile("ZChangerMMO.sqlite3", DataDirectoryHelper.DataFolderName);
                File.SetAttributes(filePath, File.GetAttributes(filePath) & ~FileAttributes.ReadOnly);
            }
            return new SQLiteConnection(new SQLiteConnectionStringBuilder { DataSource = filePath }.ConnectionString);
        }
    }
    public class MyDbSQLiteConfiguration : System.Data.Entity.DbConfiguration {
        public MyDbSQLiteConfiguration() {
            SetDefaultConnectionFactory(new SQLiteConnectionFactory());
            SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
            SetProviderFactory("System.Data.SQLite.EF6", System.Data.SQLite.EF6.SQLiteProviderFactory.Instance);
            Type t = Type.GetType("System.Data.SQLite.EF6.SQLiteProviderServices, System.Data.SQLite.EF6", true, true);
            System.Reflection.FieldInfo fi = t.GetField("Instance", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
            SetProviderServices("System.Data.SQLite", (System.Data.Entity.Core.Common.DbProviderServices)fi.GetValue(null));
        }
    }
}
