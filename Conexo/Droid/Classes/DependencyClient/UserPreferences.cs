using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Preferences;
using Common.Preferences;
using Global.Constants;

namespace Conexo.Droid.DependencyClient
{
	public class UserPreferences : IUserPreferences
	{

		Context _context;
		ISharedPreferences _prefs;
		ISharedPreferencesEditor _editor;


		public UserPreferences()
		{
			_context = LocalApp.GetInstance();
			_prefs = PreferenceManager.GetDefaultSharedPreferences(_context);
			_editor = _prefs.Edit();
		}

		#region IUserPreferences implementation

		public void StoreIntValue(string key, int value)
		{
			_editor.PutInt(key, value);
			_editor.Apply();
		}

		public void StoreStringValue(string key, string value)
		{
			_editor.PutString(key, value);
			_editor.Apply();
		}

		public int GetStoredIntValue(string key)
		{
			return _prefs.GetInt(key, 0);
		}

		public string GetStoredStringValue(string key)
		{
			return _prefs.GetString(key, "");
		}


		void IUserPreferences.StoreBoolValue(string key, bool value)
		{
			_editor.PutBoolean(key, value);
			_editor.Apply();
		}

		bool IUserPreferences.GetStoreBoolValue(string key)
		{
			return _prefs.GetBoolean(key, false);
		}

		public string GetToken()
		{
			return GetStoredStringValue(GlobalConfig.TOKEN_KEY);
		}
		#endregion
	}
}
