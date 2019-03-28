using System;
using Common.Dependencies.SQL;

namespace Infraestructure.Repositories.Base
{
	public class BaseCreateRepository<T> : BaseDataRepository, ICreateRepository<T> where T : class
	{
		public BaseCreateRepository (ISQLiteConnectionDependency sqliteConnection) : base (sqliteConnection)
		{
			DataContext.CreateTable<T> ();
		}


			
		#region ICreateRepository implementation

		public void Insert (T entity)
		{
			lock (Locker) {
				DataContext.Insert (entity);
			}
		}

		public void Insert (System.Collections.Generic.IEnumerable<T> entities)
		{
			lock (Locker) {
				DataContext.InsertAll (entities);
			}
		}

		public void DeleteAllAndInsert (System.Collections.Generic.IEnumerable<T> entities)
		{
			lock (Locker) {
				DataContext.RunInTransaction(() => {
					// database calls inside the transaction
					DataContext.DeleteAll<T>();
					DataContext.InsertAll(entities);
				});
			}
		}

		#endregion

	}
}

