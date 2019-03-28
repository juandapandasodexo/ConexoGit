using System;
using Common.Dependencies.SQL;
using SQLite.Net;

namespace Infraestructure.Repositories.Base
{
	public abstract class BaseCRUDRepository<T> : BaseDataRepository, ICRUDRepository<T> where T : class
	{

		protected TableQuery<T> Table {
			get{ 
				return DataContext.Table<T> ();
			}
		}

		private BaseCreateRepository<T> _createRepository;
		private BaseReadRepository<T> _readRepository;
		private BaseUpdateRepository<T> _updateRepository;
		private BaseDeleteRepository<T> _deleteRepository;

		public BaseCRUDRepository (ISQLiteConnectionDependency sqliteConnection):base (sqliteConnection){
			_createRepository = new BaseCreateRepository <T>(sqliteConnection);
			_readRepository = new BaseReadRepository <T>(sqliteConnection);
			_updateRepository = new BaseUpdateRepository <T>(sqliteConnection);
			_deleteRepository = new BaseDeleteRepository <T>(sqliteConnection);
		}
			
		#region IDeleteRepository implementation

		public void Delete (T entity)
		{
			_deleteRepository.Delete (entity);
		}

		public void DeleteAll ()
		{
			_deleteRepository.DeleteAll ();
		}

		#endregion

		#region IReadRepository implementation

		public T GetByPrimaryKey (object id)
		{
			return _readRepository.GetByPrimaryKey (id);
		}

		public System.Collections.Generic.List<T> GetAll ()
		{
			return _readRepository.GetAll ();
		}

		#endregion

		#region IUpdateRepository implementation

		public void Update (T entity)
		{
			_updateRepository.Update (entity);
		}

		#endregion

		#region ICreateRepository implementation

		public void Insert (T entity)
		{
			_createRepository.Insert (entity);
		}

		public void Insert (System.Collections.Generic.IEnumerable<T> entities)
		{
			_createRepository.Insert (entities);
		}

		public void DeleteAllAndInsert (System.Collections.Generic.IEnumerable<T> entities)
		{
			_createRepository.DeleteAllAndInsert (entities);
		}

		#endregion
	}
}

