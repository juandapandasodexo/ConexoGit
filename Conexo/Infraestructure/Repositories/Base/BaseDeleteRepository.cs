using System;
using Common.Dependencies.SQL;

namespace Infraestructure.Repositories.Base
{
	public class BaseDeleteRepository<T> : BaseDataRepository, IDeleteRepository<T> where T : class
	{
		public BaseDeleteRepository (ISQLiteConnectionDependency sqliteConnection) : base (sqliteConnection)
		{
			DataContext.CreateTable<T> ();
		}

		#region IDeleteRepository implementation

		public void Delete (T entity)
		{
			lock (Locker) {
				DataContext.Delete (entity);
			}

		}

		public void DeleteAll ()
		{
			lock (Locker) {
				DataContext.DeleteAll<T>();
			}
		}

		#endregion
	}
}

