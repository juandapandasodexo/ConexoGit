using System;
using SQLite.Net.Attributes;

namespace Infraestructure.Entities
{
    public class UserEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string UserName { get; set; }
        public string Password { get; set; }
		public DateTime LastAccess { get; set; }
		public bool IsRemember { get; set; }

	}
}
