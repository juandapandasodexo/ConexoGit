using System;
using Conexo.iOS.Classes.Networking;
using Common.Dependencies.Networking;

namespace Conexo.iOS.Classes.DependencyClients
{
    public class NetworkClient: INetworkDependency
    {
        public NetworkClient()
        {
        }

		#region INetworkDependency implementation

		public bool IsConnected()
		{
			if (Reachability.InternetConnectionStatus() == NetworkStatus.ReachableViaCarrierDataNetwork ||
				Reachability.InternetConnectionStatus() == NetworkStatus.ReachableViaWiFiNetwork)
			{
				return true;
			}
			return false;
		}

		#endregion
	}
}
