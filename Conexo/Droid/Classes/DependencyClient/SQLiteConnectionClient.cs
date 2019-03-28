using System;
using Common.Dependencies.SQL;
using SQLite.Net;
using System.IO;
using Configuration;
using Global.Constants;
using SQLite.Net.Platform.XamarinAndroid;

namespace Conexo.Droid.DependencyClient
{
	public class SQLiteConnectionClient : ISQLiteConnectionDependency
	{

		private static SQLiteConnection _connection;

		#region ISQLiteConnectionDependency implementation

		public SQLiteConnection GetConnection()
		{
			if (_connection == null)
			{
				string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
				string databaseFileName = Path.Combine(folder, GlobalConfig.DATABASE_NAME);

				//PreloadDbIfNeeded(databaseFileName);


				var platform = new SQLitePlatformAndroidN();
				_connection = new SQLiteConnection(platform, databaseFileName);
			}
			return _connection;
		}

		#endregion
	}
}
