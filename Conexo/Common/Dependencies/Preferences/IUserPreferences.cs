using System;
using System.Threading.Tasks;

namespace Common.Preferences
{
	public interface IUserPreferences
	{
		void StoreIntValue(string key, int value);
		int GetStoredIntValue(string key);

		void StoreStringValue(string key, string value);
		string GetStoredStringValue(string key);

		void StoreBoolValue(string key, bool value);
		bool GetStoreBoolValue(string key);

		string GetToken();
	}
}
