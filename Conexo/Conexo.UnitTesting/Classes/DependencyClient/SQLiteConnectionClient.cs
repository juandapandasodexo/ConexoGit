using System;
using System.IO;
using Common.Dependencies.SQL;
using Global.Constants;
using SQLite.Net;

namespace Conexo.UnitTesting.Classes.DependencyClient
{
    public class SQLiteConnectionClient : ISQLiteConnectionDependency
    {

        private static SQLiteConnection _connection;

        public SQLiteConnectionClient()
        {
        }

        public SQLiteConnection GetConnection()
        {
            if (_connection == null)
            {
                var folder = AppDomain.CurrentDomain.BaseDirectory;
                string databaseFileName = Path.Combine(folder, GlobalConfig.DATABASE_NAME);


                var platform = new SQLite.Net.Platform.Generic.SQLitePlatformGeneric();
                _connection = new SQLite.Net.SQLiteConnection(platform, databaseFileName);
            }


            return _connection;
        }
    }
}
