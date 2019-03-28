using System;

namespace Infraestructure.Repositories.Base
{
	public interface ICRUDRepository<T> : ICreateRepository<T>,IUpdateRepository<T>,IReadRepository<T>,IDeleteRepository<T> where T : class
	{
	}
}

