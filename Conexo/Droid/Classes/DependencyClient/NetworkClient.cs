using System;
using Android.Net;
using Common.Dependencies.Networking;

namespace Conexo.Droid.DependencyClient
{
	public class NetworkClient : INetworkDependency
	{
		
		public bool IsConnected()
		{
			ConnectivityManager connectivityManager = (ConnectivityManager)LocalApp.GetInstance().GetSystemService(LocalApp.ConnectivityService);
			NetworkInfo activeConnection = connectivityManager.ActiveNetworkInfo;
			bool isOnline = (activeConnection != null) && activeConnection.IsConnected;
			return isOnline;
		}
	}
}
