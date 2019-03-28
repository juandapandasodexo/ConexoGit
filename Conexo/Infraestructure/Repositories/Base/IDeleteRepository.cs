using System;

namespace Infraestructure.Repositories.Base
{
	public interface IDeleteRepository<T> where T: class
	{
		void Delete(T entity);
		void DeleteAll();
	}
}

