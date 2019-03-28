using System;
using SQLite.Net.Attributes;

namespace Infraestructure.Entities
{
	public class TokenEntity
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public byte[] TokenCache { get; set; }

	}
}
