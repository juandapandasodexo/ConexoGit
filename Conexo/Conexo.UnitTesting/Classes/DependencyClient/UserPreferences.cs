using System;
using System.Collections.Generic;
using Common.Preferences;
using Global.Constants;

namespace Conexo.UnitTesting.Classes.DependencyClient
{
    public class UserPreferences : IUserPreferences
    {

        private static Dictionary<string, string> StringProperties { get; set; } = new Dictionary<string, string>();
        private static Dictionary<string, Boolean> BoolProperties { get; set; } = new Dictionary<string, Boolean>();
        private static Dictionary<string, int> IntProperties { get; set; } = new Dictionary<string, int>();



        public UserPreferences()
        {
        }

        public bool GetStoreBoolValue(string key)
        {
            return BoolProperties[key];
        }

        public int GetStoredIntValue(string key)
        {
            return IntProperties[key];
        }

        public string GetStoredStringValue(string key)
        {
            return StringProperties[key];
        }

        public string GetToken()
        {
            return GetStoredStringValue(GlobalConfig.TOKEN_KEY);
        }

        public void StoreBoolValue(string key, bool value)
        {
            BoolProperties[key] = value;
        }

        public void StoreIntValue(string key, int value)
        {
            IntProperties[key] = value;
        }

        public void StoreStringValue(string key, string value)
        {
            StringProperties[key] = value;
        }
    }

}
