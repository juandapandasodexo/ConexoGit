using System;
using Common.Dependencies.SQL;

namespace Infraestructure.Repositories.Base
{
	public class BaseUpdateRepository<T> : BaseDataRepository, IUpdateRepository<T> where T : class
	{
		public BaseUpdateRepository (ISQLiteConnectionDependency sqliteConnection) : base (sqliteConnection)
		{
			DataContext.CreateTable<T> ();
		}
			
		#region IUpdateRepository implementation

		public void Update (T entity)
		{
			lock (Locker) {
				DataContext.Update (entity);
			}
		}

		#endregion
	}
}

