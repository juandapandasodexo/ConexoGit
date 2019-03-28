using System;
using System.Collections.Generic;

namespace Infraestructure.Repositories.Base
{
	public interface ICreateRepository<T> where T: class
	{
		void Insert(T entity);
		void Insert(IEnumerable<T> entities);
		void DeleteAllAndInsert(IEnumerable<T> entities);
	}
}

