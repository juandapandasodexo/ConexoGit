using System;
namespace Common.Dependencies.Firebase
{
    public interface IFirebaseAnalyticsDependency
    {
        void SetScreenNameAndClass(string screenName, string screenClassOverride);
    }
}
