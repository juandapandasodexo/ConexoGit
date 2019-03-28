using System;
using Common.Dependencies.SQL;
using SQLite.Net;

namespace Infraestructure.Repositories.Base
{
	public class BaseDataRepository
	{

		private static object _locker;
		protected static object Locker{
			get{ 
				if (_locker == null) {
					_locker = new object ();
				}
				return _locker;
			}
		}

		protected SQLiteConnection DataContext{
			get;
			set;
		}


		public BaseDataRepository (ISQLiteConnectionDependency sqliteConnection)
		{
			this.DataContext = sqliteConnection.GetConnection();

		}
	}
}

