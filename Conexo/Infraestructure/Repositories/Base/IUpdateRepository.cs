﻿using System;

namespace Infraestructure.Repositories.Base
{
	public interface IUpdateRepository<T> where T: class
	{
			void Update(T entity);
	}
}

