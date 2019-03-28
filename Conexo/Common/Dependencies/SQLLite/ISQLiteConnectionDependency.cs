using System;
using SQLite.Net;

namespace Common.Dependencies.SQL
{
	public interface ISQLiteConnectionDependency
	{
		SQLiteConnection GetConnection();
	}
}

