using System;
using Android.App;
using Android.Content;
using Common.Dependencies.Firebase;

namespace Conexo.Droid.Classes.DependencyClient
{
    public class FirebaseAnalyticsClient : IFirebaseAnalyticsDependency
    {
        public FirebaseAnalyticsClient()
        {
        }

        public void SetScreenNameAndClass(string screenName, string screenClassOverride)
        {

            var context = MainActivity.Instance;
            var fbInstance = Firebase.Analytics.FirebaseAnalytics.GetInstance(context);
            fbInstance.SetCurrentScreen(context, screenName, screenClassOverride);
        }
    }
}
