using System;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using System.IO;
using Global.Constants;
using Common.Dependencies.SQL;

namespace Conexo.iOS.Classes.DependencyClients
{
    public class SQLiteConnectionClient: ISQLiteConnectionDependency
    {

        private static SQLiteConnection _connection;

        public SQLiteConnectionClient()
        {
        }

		#region ISQLiteConnectionDependency implementation

		public SQLiteConnection GetConnection()
		{
			if (_connection == null)
			{
				string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				string databaseFileName = Path.Combine(folder, GlobalConfig.DATABASE_NAME);
				var platform = new SQLitePlatformIOS();
				_connection = new SQLiteConnection(platform, databaseFileName);
			}
			return _connection;
		}

		#endregion
	}
}
