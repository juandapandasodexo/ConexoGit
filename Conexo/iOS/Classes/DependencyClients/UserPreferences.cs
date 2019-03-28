using System;
using System.Threading.Tasks;
using Common.Preferences;
using Global.Constants;
using Foundation;

namespace Conexo.iOS.Classes.DependencyClients
{
    public class UserPreferences: IUserPreferences
    {
        public UserPreferences()
        {
        }


		#region IUserPreferences implementation

		public void StoreIntValue(string key, int value)
		{
			NSUserDefaults.StandardUserDefaults.SetInt(value, key);
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}

		public void StoreStringValue(string key, string value)
		{
			NSUserDefaults.StandardUserDefaults.SetString(value, key);
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}

		public int GetStoredIntValue(string key)
		{
			string storeStr = NSUserDefaults.StandardUserDefaults.IntForKey(key).ToString();
			if (string.IsNullOrEmpty(storeStr))
			{
				return 0;
			}
			return Int32.Parse(storeStr);
		}

		public string GetStoredStringValue(string key)
		{
			return NSUserDefaults.StandardUserDefaults.StringForKey(key);
		}

		public void StoreBoolValue(string key, bool value)
		{
			NSUserDefaults.StandardUserDefaults.SetBool(value, key);
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}

		public bool GetStoreBoolValue(string key)
		{
			return NSUserDefaults.StandardUserDefaults.BoolForKey(key);
		}

		public string GetToken()
		{
			return GetStoredStringValue(GlobalConfig.TOKEN_KEY);
		}

		#endregion
	}
}
