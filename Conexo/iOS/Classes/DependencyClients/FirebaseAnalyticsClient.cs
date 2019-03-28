using System;
using Common.Dependencies.Firebase;

namespace Conexo.iOS.Classes.DependencyClients
{
    public class FirebaseAnalyticsClient : IFirebaseAnalyticsDependency
    {
        public FirebaseAnalyticsClient()
        {
        }

        public void SetScreenNameAndClass(string screenName, string screenClassOverride)
        {
            Firebase.Analytics.Analytics.SetScreenNameAndClass(screenName, screenClassOverride);
        }
    }
}
