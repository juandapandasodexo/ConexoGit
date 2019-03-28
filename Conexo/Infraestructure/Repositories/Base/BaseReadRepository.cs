using System;
using Common.Dependencies.SQL;
using System.Linq;

namespace Infraestructure.Repositories.Base
{
	public class BaseReadRepository<T> : BaseDataRepository, IReadRepository<T> where T : class
	{
		public BaseReadRepository (ISQLiteConnectionDependency sqliteConnection) : base (sqliteConnection)
		{
			DataContext.CreateTable<T> ();
		}

		#region IReadRepository implementation

		public T GetByPrimaryKey(object id)
		{
			lock (Locker) {
				return DataContext.Get<T> (id);
			}
		}

		public System.Collections.Generic.List<T> GetAll ()
		{
			lock (Locker) {
				return DataContext.Table<T> ().ToList ();
			}
		}

		#endregion
	}
}

