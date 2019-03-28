using Common.Dependencies.SQL;
using Infraestructure.Entities;
using Infraestructure.Repositories.Base;

namespace Infraestructure.Repositories.Data
{

    public interface IUserDataRepository : ICRUDRepository<UserEntity>
	{ 
		UserEntity GetUser();
	}


	public class UserDataRepository : BaseCRUDRepository<UserEntity>, IUserDataRepository
	{
		public UserDataRepository(ISQLiteConnectionDependency sqliteConnection) : base(sqliteConnection)
		{
		}

		public UserEntity GetUser()
		{
			var user = default(UserEntity);
			lock (Locker)
			{
				user = Table.FirstOrDefault();
			}
			return user;			
		}

	}

}
