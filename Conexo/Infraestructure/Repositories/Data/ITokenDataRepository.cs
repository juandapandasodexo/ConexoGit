using System;
using System.Linq;
using Common.Dependencies.SQL;
using Infraestructure.Entities;
using Infraestructure.Repositories.Base;

namespace Infraestructure.Repositories.Data
{
	public interface ITokenDataRepository : ICRUDRepository<TokenEntity>
	{ 
		TokenEntity GetToken();
	}

	public class TokenDataRepository : BaseCRUDRepository<TokenEntity>, ITokenDataRepository
	{
		public TokenDataRepository(ISQLiteConnectionDependency sqliteConnection) : base(sqliteConnection)
		{
		}

		public TokenEntity GetToken()
		{
			lock(Locker)
			{
				return Table.FirstOrDefault();
			}
		}
	}
}
